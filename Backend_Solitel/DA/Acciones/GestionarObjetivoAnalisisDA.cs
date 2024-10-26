using BC.Modelos;
using BC.Reglas_de_Negocio;
using BW.Interfaces.DA;
using DA.Contexto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Acciones
{
    public class GestionarObjetivoAnalisisDA : IGestionarObjetivoAnalisisDA
    {
        private readonly SolitelContext _context;

        public GestionarObjetivoAnalisisDA(SolitelContext context)
        {
            _context = context;
        }

        public async Task<bool> EliminarObjetivoAnalisis(int idObjetivoAnalisis)
        {
            try
            {
                // Definir el parámetro para el procedimiento almacenado
                var idObjetivoAnalisisParam = new SqlParameter("@PN_IdObjetivoAnalisis", idObjetivoAnalisis);

                // Ejecutar el procedimiento almacenado para eliminar (lógicamente)
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_EliminarObjetivoAnalisis @PN_IdObjetivoAnalisis", idObjetivoAnalisisParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al eliminar el objetivoAnalisis.");
                }

                return resultado >= 0 ? true : false;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al eliminar el objetivoAnalisis: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al eliminar el objetivoAnalisis: {ex.Message}", ex);
            }
        }

        public async Task<ObjetivoAnalisis> InsertarObjetivoAnalisis(ObjetivoAnalisis objetivoAnalisis)
        {
            try
            {
                // Definir los parámetros para el procedimiento almacenado
                var nombreParam = new SqlParameter("@PC_Nombre", objetivoAnalisis.TC_Nombre);
                var descripcionParam = new SqlParameter("@PC_Descripcion", objetivoAnalisis.TC_Descripcion);

                // Definir el parámetro de salida para capturar el ID generado
                var idParam = new SqlParameter("@pTN_IdObjetivoAnalisis", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarObjetivoAnalisis @pTN_IdObjetivoAnalisis OUTPUT, @PC_Nombre, @PC_Descripcion",
                    idParam, nombreParam, descripcionParam
                );

                // Capturar el ID generado desde el parámetro de salida
                var nuevoId = (int)idParam.Value;

                if (nuevoId <= 0)
                {
                    throw new Exception("Error al obtener el ID del objetivo de análisis recién insertado.");
                }

                // Asignar el ID generado a la entidad ObjetivoAnalisis
                objetivoAnalisis.TN_IdObjetivoAnalisis = nuevoId;

                return objetivoAnalisis;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al insertar el objetivo de análisis: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar el objetivo de análisis: {ex.Message}", ex);
            }
        }

        public async Task<List<ObjetivoAnalisis>> ObtenerObjetivoAnalisis(int idObjetivoAnalisis)
        {
            try
            {
                // Definir el parámetro
                var TN_IdObjetivoAnalisis = new SqlParameter("@pTN_IdObjetivoAnalisis", (idObjetivoAnalisis>0 && idObjetivoAnalisis != null)? idObjetivoAnalisis:null);

                // Ejecutar el procedimiento almacenado pasando el parámetro
                var ObjetivoAnalisisDA = await _context.tSOLITEL_ObjetivoAnalisisDA
                    .FromSqlRaw("EXEC PA_ObtenerObjetivoAnalisis @pTN_IdObjetivoAnalisis", TN_IdObjetivoAnalisis)
                    .ToListAsync();

                // Mapear el resultado a la lista de ObjetivoAnalisis
                var objetivos = ObjetivoAnalisisDA.Select(obj => new ObjetivoAnalisis
                {
                    TN_IdObjetivoAnalisis = obj.TN_IdObjetivoAnalisis,
                    TC_Nombre = obj.TC_Nombre,
                    TC_Descripcion = obj.TC_Descripcion
                }).ToList();

                return objetivos;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error en la base de datos al obtener Objetivos: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error inesperado al obtener la lista de Objetivos: {ex.Message}", ex);
            }
        }

    }
}
