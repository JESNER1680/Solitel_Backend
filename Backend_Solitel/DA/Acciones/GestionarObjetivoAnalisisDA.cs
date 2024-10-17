using BC.Modelos;
using BC.Reglas_de_Negocio;
using BW.Interfaces.DA;
using DA.Contexto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                var idObjetivoAnalisisParam = new SqlParameter("@PC_IdObjetivoAnalisis", idObjetivoAnalisis);

                // Ejecutar el procedimiento almacenado para eliminar (lógicamente)
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_EliminarObjetivoAnalisis @PC_IdObjetivoAnalisis", idObjetivoAnalisisParam);

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

        public async Task<bool> InsertarObjetivoAnalisis(ObjetivoAnalisis objetivoAnalisis)
        {
            try
            {
                // Definir los parámetros para el procedimiento almacenado
                var nombreParam = new SqlParameter("@PC_Nombre", objetivoAnalisis.TC_Nombre);
                var descripcionParam = new SqlParameter("@PC_Descripcion", objetivoAnalisis.TC_Descripcion);

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarObjetivoAnalisis @PC_Nombre, @PC_Descripcion",
                    nombreParam, descripcionParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al insertar el objetivo de analisis.");
                }

                return resultado >= 0 ? true : false;
            }
            catch (SqlException ex)
            {
                // Si el error proviene de SQL Server, se captura el mensaje del procedimiento almacenado
                throw new Exception($"Error en la base de datos al insertar el objetivo de analisis: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar el objetivo de analisis: {ex.Message}", ex);
            }
        }
    }
}
