using BC.Modelos;
using BW.Interfaces.BW;
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

        public async Task<bool> InsertarTipoAnalisis(TipoAnalisis tipoAnalisis)
        {
            try
            {
                // Definir los parámetros para el procedimiento almacenado
                var nombreParam = new SqlParameter("@PC_Nombre", tipoAnalisis.TC_Nombre);
                var descripcionParam = new SqlParameter("@PC_Descripcion", tipoAnalisis.TC_Descripcion);

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsetarTipoAnalisis @PC_Nombre, @PC_Descripcion",
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
                throw new Exception($"Error en la base de datos al insertar el Tipo de analisis: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar el Tipo de analisis: {ex.Message}", ex);
            }
        }
    }
}
