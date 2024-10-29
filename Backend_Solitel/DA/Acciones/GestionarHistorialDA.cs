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
    public class GestionarHistorialDA : IGestionarHistorialDA
    {
        private readonly SolitelContext _context;

        public GestionarHistorialDA(SolitelContext solitelContext)
        {
            _context = solitelContext;
        }
    
        public async Task<List<Historial>> ConsultarHistorialDeSolicitudProveedor(int idSolicitudProveedor)
        {
            try
            {
                var idSolicitudProveedorParam = new SqlParameter("@PN_IdSolicitudProveedor", idSolicitudProveedor);

                // Ejecutar el procedimiento almacenado
                var historialesDA = await _context.TSOLITEL_HistorialDA
                    .FromSqlRaw("EXEC PA_ConsultarHistoricoSolicitudProveedor @PN_IdSolicitudProveedor", idSolicitudProveedorParam)
                    .ToListAsync();

                // Mapeo de los resultados
                var historiales = historialesDA.Select(da => new Historial
                {
                    TN_IdHistorial = da.TN_IdHistorial,
                    TC_Observacion = da.TC_Observacion,
                    TF_FechaEstado = da.TF_FechaEstado,
                    TN_IdAnalisis = da.TN_IdAnalisis,
                    TN_IdEstado = da.TN_IdEstado,
                    TN_IdUsuario = da.TN_IdUsuario,
                    TN_IdSolicitudProveedor = da.TN_IdSolicitudProveedor

                }).ToList();

                return historiales;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener historiales de solicitud de proveedor: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener historiales de solicitud de proveedor: {ex.Message}", ex);
            }
        }
    }
}
