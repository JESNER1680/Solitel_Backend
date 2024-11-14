using BC.Modelos;
using BW.Interfaces.DA;
using DA.Contexto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DA.Acciones
{
    public class GestionarAsignacionDA : IGestionarAsignacionDA
    {
        private readonly SolitelContext _context;

        public GestionarAsignacionDA(SolitelContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertarAsignacion(int idSolicitudAnalisis, int idUsuario)
        {
            try
            {
                // Definir los parámetros para el procedimiento almacenado
                var idSolicitudAnalisisParam = new SqlParameter("@pTN_IdAnalisis", idSolicitudAnalisis);
                var idUsuarioParam = new SqlParameter("@pTN_IdUsuario", idUsuario);

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarAsignacion @pTN_IdAnalisis, @pTN_IdUsuario, ",
                    idSolicitudAnalisisParam, idUsuarioParam );

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al insertar la asignacion.");
                }

                return resultado >= 0 ? true : false;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al insertar la asignacion: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar la asignacion: {ex.Message}", ex);
            }
        }
    }
}
