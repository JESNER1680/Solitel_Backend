using BC.Modelos;
using BW.Interfaces.BW;
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
    public class GestionarTipoAnalisisDA : IGestionarTipoAnalisisDA
    {
        private readonly SolitelContext _context;

        public GestionarTipoAnalisisDA(SolitelContext context)
        {
            _context = context;
        }
        public async Task<bool> EliminarTipoAnalisis(int idTipoAnalisis)
        {
            try
            {
                // Definir el parámetro para el procedimiento almacenado
                var idTipoAnalisisParam = new SqlParameter("@PN_IdTipoAnalisis", idTipoAnalisis);

                // Ejecutar el procedimiento almacenado para eliminar (lógicamente)
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_EliminarTipoAnalisis @PN_IdTipoAnalisis", idTipoAnalisisParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al eliminar el Tipo Analisis.");
                }

                return resultado >= 0 ? true : false;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al eliminar el Tipo Analisis: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al eliminar el Tipo Analisis: {ex.Message}", ex);
            }
        }

        public async Task<TipoAnalisis> InsertarTipoAnalisis(TipoAnalisis tipoAnalisis)
        {
            try
            {
                // Definir los parámetros para el procedimiento almacenado
                var nombreParam = new SqlParameter("@pTC_Nombre", tipoAnalisis.TC_Nombre);
                var descripcionParam = new SqlParameter("@pTC_Descripcion", tipoAnalisis.TC_Descripcion);

                // Definir el parámetro de salida para capturar el ID generado
                var idParam = new SqlParameter("@pTN_IdTipoAnalisis", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarTipoAnalisis @pTN_IdTipoAnalisis OUTPUT, @pTC_Nombre, @pTC_Descripcion",
                    idParam, nombreParam, descripcionParam
                );

                // Capturar el ID generado desde el parámetro de salida
                var nuevoId = (int)idParam.Value;
                if (nuevoId <= 0)
                {
                    throw new Exception("Error al obtener el ID del tipo de análisis recién insertado.");
                }

                // Asignar el ID generado a la entidad TipoAnalisis
                tipoAnalisis.TN_IdTipoAnalisis = nuevoId;

                return tipoAnalisis;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al insertar el tipo de análisis: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar el tipo de análisis: {ex.Message}", ex);
            }
        }

        public async Task<List<TipoAnalisis>> obtenerTipoAnalisis()
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar todos los tipos de análisis
                var tipoAnalisisDA = await _context.TSOLITEL_TipoAnalisisDA
                    .FromSqlRaw("EXEC PA_ConsultarTipoAnalisis")
                    .ToListAsync();

                // Mapeo de los resultados a la entidad TipoAnalisis
                var tipoAnalisisList = tipoAnalisisDA.Select(ta => new TipoAnalisis
                {
                    TN_IdTipoAnalisis = ta.TN_IdTipoAnalisis,
                    TC_Nombre = ta.TC_Nombre,
                    TC_Descripcion = ta.TC_Descripcion
                }).ToList();

                return tipoAnalisisList;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener tipos de análisis: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener la lista de tipos de análisis: {ex.Message}", ex);
            }
        }

        public async Task<TipoAnalisis> obtenerTipoAnalisis(int id)
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar el tipo de análisis por ID
                var tipoAnalisisDA = await _context.TSOLITEL_TipoAnalisisDA
                    .FromSqlRaw("EXEC PA_ConsultarTipoAnalisis @pTN_IdTipoAnalisis = {0}", id)
                    .ToListAsync();  // Convertir la consulta a una lista

                var tipoAnalisis = tipoAnalisisDA.FirstOrDefault();  // Obtener el primer elemento si existe

                // Verificar si se encontró el tipo de análisis
                if (tipoAnalisis == null)
                {
                    throw new Exception($"No se encontró un tipo de análisis con el ID {id}.");
                }

                // Mapear los resultados a la entidad TipoAnalisis
                var tipoAnalisisEntidad = new TipoAnalisis
                {
                    TN_IdTipoAnalisis = tipoAnalisis.TN_IdTipoAnalisis,
                    TC_Nombre = tipoAnalisis.TC_Nombre,
                    TC_Descripcion = tipoAnalisis.TC_Descripcion
                };

                return tipoAnalisisEntidad;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener el tipo de análisis con ID {id}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener el tipo de análisis con ID {id}: {ex.Message}", ex);
            }
        }

    }
}
