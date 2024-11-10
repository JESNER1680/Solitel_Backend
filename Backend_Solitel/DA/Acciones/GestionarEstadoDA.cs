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
    public class GestionarEstadoDA : IGestionarEstadoDA
    {
        private readonly SolitelContext _context;

        public GestionarEstadoDA(SolitelContext solitelContext)
        {
            _context = solitelContext;
        }

        public async Task<List<Estado>> ObtenerEstados(int idUsuario, int idOficina)
        {
            try
            {
                var idUsuarioParam = new SqlParameter("@pTN_IdUsuario", idUsuario);
                var idOficinaParam = new SqlParameter("@pTN_IdOficina", idOficina);

                // Ejecutar el procedimiento almacenado
                var estadosDA = await _context.TSOLITEL_EstadoDA
                    .FromSqlRaw("EXEC PA_ConsultarEstados, @pTN_IdUsuario , @pTN_IdUsuario", idUsuarioParam, idOficinaParam)
                    .ToListAsync();

                // Mapeo de los resultados
                var estados = estadosDA.Select(da => new Estado
                {
                    IdEstado = da.TN_IdEstado,
                    Nombre = da.TC_Nombre,
                    Descripcion = da.TC_Descripcion,
                    Tipo = da.TC_Tipo,
                    CantidadSolicitudes = da.TN_CantidadSolicitudes

                }).ToList();

                return estados;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener estados: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener la lista de estados: {ex.Message}", ex);
            }
        }
    }
}
