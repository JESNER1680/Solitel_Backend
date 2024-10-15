using BC.Modelos;
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
    public class GestionarSubModalidadDA : IGestionarSubModalidadDA
    {
        private readonly SolitelContext _context;

        public GestionarSubModalidadDA(SolitelContext context)
        {
            _context = context;
        }

        public async Task<SubModalidad> insertarSubModalidad(SubModalidad subModalidad)
        {
            try
            {
                // Definir los parámetros para el procedimiento almacenado
                var nombreParam = new SqlParameter("@pTC_Nombre", subModalidad.TC_Nombre);
                var descripcionParam = new SqlParameter("@pTC_Descripcion", subModalidad.TC_Descripcion);
                var modalidadParam = new SqlParameter("@pTN_IdModalidad", subModalidad.TN_IdModalida);

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarSubModalidad @pTC_Nombre, @pTC_Descripcion, @pTN_IdModalidad",
                    nombreParam, descripcionParam, modalidadParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al insertar la submodalidad.");
                }

                return subModalidad;
            }
            catch (SqlException ex)
            {
                // Si el error proviene de SQL Server, se captura el mensaje del procedimiento almacenado
                throw new Exception($"Error en la base de datos al insertar la submodalidad: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar la submodalidad: {ex.Message}", ex);
            }
        }

        public async Task<List<SubModalidad>> obtenerSubModalidad()
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar submodalidades
                var subModalidadesDA = await _context.TSOLITEL_SubModalidadDA
                    .FromSqlRaw("EXEC PA_ConsultarSubModalidad")
                    .ToListAsync();

                // Mapeo de los resultados a la entidad SubModalidad
                var subModalidades = subModalidadesDA.Select(da => new SubModalidad
                {
                    TN_IdSubModalidad = da.TN_IdSubModalidad,
                    TC_Nombre = da.TC_Nombre,
                    TC_Descripcion = da.TC_Descripcion,
                    TN_IdModalida = da.TN_IdModalida,
                }).ToList();

                return subModalidades;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener las submodalidades: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener las submodalidades: {ex.Message}", ex);
            }
        }

        public async Task<SubModalidad> eliminarSubModalidad(int id)
        {
            try
            {
                // Definir el parámetro para el procedimiento almacenado
                var idSubModalidadParam = new SqlParameter("@pTN_IdSubModalidad", id);

                // Ejecutar el procedimiento almacenado para eliminar (lógicamente)
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_EliminarSubModalidad @pTN_IdSubModalidad", idSubModalidadParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al eliminar la submodalidad.");
                }

                // Retornar un objeto SubModalidad con el Id de la submodalidad eliminada
                return new SubModalidad { TN_IdSubModalidad = id };
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al eliminar la submodalidad: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al eliminar la submodalidad: {ex.Message}", ex);
            }
        }
    }
}
