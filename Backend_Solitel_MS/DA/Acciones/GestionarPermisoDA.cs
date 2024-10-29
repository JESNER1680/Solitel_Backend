using BW.Interfaces.DA;
using DA.Contexto;
using DA.Entidades;
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
    public class GestionarPermisoDA : IGestionarPermisoDA
    {

        private readonly SolitelContext _context;

        public GestionarPermisoDA (SolitelContext context)
        {
            _context = context;
        }

        public async Task<bool> EliminarPermiso(int id)
        {
            try
            {
                // Definir el parámetro para el procedimiento almacenado
                var idParam = new SqlParameter("@pTN_IdPermiso", id);

                // Ejecutar el procedimiento almacenado
                await _context.Database.ExecuteSqlRawAsync("EXEC PA_EliminarPermiso @pTN_IdPermiso", idParam);

                return true; // Retorna true si la operación fue exitosa
            }
            catch (SqlException ex)
            {
                // Manejar el error específico de SQL Server
                throw new Exception($"Error al eliminar el permiso: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejar cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al eliminar el permiso: {ex.Message}", ex);
            }
        }

        public async Task<Permiso> InsertarPermiso(Permiso permiso)
        {
            try
            {
                // Definir los parámetros para el procedimiento almacenado
                var nombreParam = new SqlParameter("@pTC_Nombre", permiso.TC_Nombre);
                var descripcionParam = new SqlParameter("@pTC_Descripcion", permiso.TC_Descripcion);

                // Parámetro de salida para capturar el ID generado
                var idParam = new SqlParameter("@pTN_IdPermiso", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarPermiso @pTN_IdPermiso OUTPUT, @pTC_Nombre, @pTC_Descripcion",
                    idParam, nombreParam, descripcionParam
                );

                // Asignar el ID generado al permiso
                permiso.TN_IdPermiso = (int)idParam.Value;

                return permiso;
            }
            catch (SqlException ex)
            {
                // Manejar el error específico de SQL Server
                throw new Exception($"Error al insertar el permiso: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejar cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar el permiso: {ex.Message}", ex);
            }
        }

        public async Task<List<Permiso>> ObtenerPermisos()
        {
            try
            {
                // Ejecutar el procedimiento almacenado
                var permisos = await _context.TSOLITEL_PermisoDA
                    .FromSqlRaw("EXEC PA_ConsultarPermiso")
                    .Select(pe => new Permiso(){ 
                        TN_IdPermiso = pe.TN_IdPermiso,
                        TC_Nombre = pe.TC_Nombre,
                        TC_Descripcion = pe.TC_Descripcion,
                    })
                    .ToListAsync();

                return permisos;
            }
            catch (SqlException ex)
            {
                // Manejar el error específico de SQL Server
                throw new Exception($"Error al obtener los permisos: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejar cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener los permisos: {ex.Message}", ex);
            }
        }

        public async Task<Permiso> ObtenerPermiso(int id)
        {
            try
            {
                // Definir el parámetro para el procedimiento almacenado
                var idParam = new SqlParameter("@pTN_IdPermiso", id);

                // Ejecutar el procedimiento almacenado con el parámetro
                var permiso = await _context.TSOLITEL_PermisoDA
                    .FromSqlRaw("EXEC PA_ConsultarPermiso @pTN_IdPermiso", idParam)
                    .Select(pe => new Permiso()
                    {
                        TN_IdPermiso = pe.TN_IdPermiso,
                        TC_Nombre = pe.TC_Nombre,
                        TC_Descripcion = pe.TC_Descripcion,
                    })
                    .FirstOrDefaultAsync();

                if (permiso == null)
                {
                    throw new Exception($"No se encontró un permiso con el ID {id}.");
                }

                return permiso;
            }
            catch (SqlException ex)
            {
                // Manejar el error específico de SQL Server
                throw new Exception($"Error al obtener el permiso con ID {id}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejar cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener el permiso con ID {id}: {ex.Message}", ex);
            }
        }

        public Task<Permiso> ObtenerPersmiso(int id)
        {
            throw new NotImplementedException();
        }
    }
}
