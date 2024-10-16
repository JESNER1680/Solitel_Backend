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
                var nombreParam = new SqlParameter("@pTC_Nombre", tipoSolicitud.TC_Nombre);
                var descripcionParam = new SqlParameter("@pTC_Descripcion", tipoSolicitud.TC_Descripcion);

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarTipoSolicitud @pTC_Nombre, @pTC_Descripcion",
                    nombreParam, descripcionParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al insertar el tipo de solicitud.");
                }

                return tipoSolicitud;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error en la base de datos al insertar el tipo de solicitud: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
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
                    TN_IdTipoSolicitud = da.TN_IdTipoSolicitud,
                    TC_Nombre = da.TC_Nombre,
                    TC_Descripcion = da.TC_Descripcion,
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

        public async Task<TipoSolicitud> eliminarTipoSolicitud(int id)
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

                return new TipoSolicitud { TN_IdTipoSolicitud = id };
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
    }
}
