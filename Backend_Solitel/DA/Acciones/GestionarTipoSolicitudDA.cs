using BC.Modelos;
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
    public class GestionarTipoSolicitudDA: IGestionarTipoSolicitudDA
    {
        private readonly SolitelContext _context;

        public GestionarTipoSolicitudDA(SolitelContext context)
        {
            _context = context;
        }

        public async Task<TipoSolicitud> insertarTipoSolicitud(TipoSolicitud tipoSolicitud)
        {
            try
            {
                // Definir los parámetros para el procedimiento almacenado
                var nombreParam = new SqlParameter("@pTC_Nombre", tipoSolicitud.Nombre);
                var descripcionParam = new SqlParameter("@pTC_Descripcion", tipoSolicitud.Descripcion);

                // Definir el parámetro de salida para capturar el ID generado
                var idParam = new SqlParameter("@pTN_IdTipoSolicitud", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarTipoSolicitud @pTN_IdTipoSolicitud OUTPUT, @pTC_Nombre, @pTC_Descripcion",
                    idParam, nombreParam, descripcionParam
                );

                // Capturar el ID generado desde el parámetro de salida
                var nuevoId = (int)idParam.Value;

                if (nuevoId <= 0)
                {
                    throw new Exception("Error al obtener el ID del tipo de solicitud recién insertado.");
                }

                // Asignar el ID generado a la entidad TipoSolicitud
                tipoSolicitud.IdTipoSolicitud = nuevoId;

                return tipoSolicitud;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al insertar el tipo de solicitud: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar el tipo de solicitud: {ex.Message}", ex);
            }
        }

        public async Task<List<TipoSolicitud>> obtenerTipoSolicitud()
        {
            try
            {
                var tiposSolicitudDA = await _context.TSOLITEL_TipoSolicitudDA
                    .FromSqlRaw("EXEC PA_ConsultarTipoSolicitud")
                    .ToListAsync();

                var tiposSolicitud = tiposSolicitudDA.Select(da => new TipoSolicitud
                {
                    IdTipoSolicitud = da.TN_IdTipoSolicitud,
                    Nombre = da.TC_Nombre,
                    Descripcion = da.TC_Descripcion,
                }).ToList();

                return tiposSolicitud;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error en la base de datos al obtener los tipos de solicitud: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error inesperado al obtener los tipos de solicitud: {ex.Message}", ex);
            }
        }

        public async Task<bool> eliminarTipoSolicitud(int id)
        {
            try
            {
                var idSolicitudParam = new SqlParameter("@pTN_IdTipoSolicitud", id);

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_EliminarTipoSolicitud @pTN_IdTipoSolicitud", idSolicitudParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al eliminar el tipo de solicitud.");
                }

                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error en la base de datos al eliminar el tipo de solicitud: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error inesperado al eliminar el tipo de solicitud: {ex.Message}", ex);
            }
        }

        public async Task<TipoSolicitud> obtenerTipoSolicitud(int id)
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar un tipo de solicitud por ID
                var tipoSolicitudDA = await _context.TSOLITEL_TipoSolicitudDA
                    .FromSqlRaw("EXEC PA_ConsultarTipoSolicitud @pTN_IdTipoSolicitud = {0}", id)
                    .ToListAsync();  // Obtener el resultado como lista

                var tipoSolicitud = tipoSolicitudDA.FirstOrDefault();  // Obtener el primer elemento si hay uno

                // Verificar si se encontró el tipo de solicitud
                if (tipoSolicitud == null)
                {
                    throw new Exception($"No se encontró un tipo de solicitud con el ID {id}.");
                }

                // Mapear el resultado a la entidad TipoSolicitud
                var tipoSolicitudResult = new TipoSolicitud
                {
                    IdTipoSolicitud = tipoSolicitud.TN_IdTipoSolicitud,
                    Nombre = tipoSolicitud.TC_Nombre,
                    Descripcion = tipoSolicitud.TC_Descripcion
                };

                return tipoSolicitudResult;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener el tipo de solicitud con ID {id}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener el tipo de solicitud con ID {id}: {ex.Message}", ex);
            }
        }
    }
}
