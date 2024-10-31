﻿using BC.Modelos;
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
                    IdHistorial = da.TN_IdHistorial,
                    Observacion = da.TC_Observacion,
                    FechaEstado = da.TF_FechaDeModificacion,
                    IdAnalisis = da.TN_IdAnalisis,
                    IdEstado = da.TN_IdEstado,
                    IdUsuario = da.TN_IdUsuario,
                    IdSolicitudProveedor = da.TN_IdSolicitud

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
