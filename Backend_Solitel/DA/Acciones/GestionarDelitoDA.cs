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
    public class GestionarDelitoDA: IGestionarDelitoDA
    {
        private readonly SolitelContext _context;

        public GestionarDelitoDA(SolitelContext context)
        {
            _context = context;
        }

        public async Task<Delito> eliminarDelito(int id)
        {
            try
            {
                // Definir el parámetro para el procedimiento almacenado
                var idDelitoParam = new SqlParameter("@pTN_IdDelito", id);

                // Ejecutar el procedimiento almacenado para eliminar (lógicamente)
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_EliminarDelito @pTN_IdDelito", idDelitoParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al eliminar el delito.");
                }

                // Retornar un objeto Delito con el Id del delito eliminado
                return new Delito { TN_IdDelito = id };
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al eliminar delito: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al eliminar el delito: {ex.Message}", ex);
            }
        }

        public async Task<Delito> insertarDelito(Delito delito)
        {
            try
            {
                // Definir los parámetros para el procedimiento almacenado
                var nombreParam = new SqlParameter("@pTC_Nombre", delito.TC_Nombre);
                var descripcionParam = new SqlParameter("@pTC_Descripcion", delito.TC_Descripcion);
                var categoriaDelitoParam = new SqlParameter("@pTN_IdCategoriaDelito", delito.TN_IdCategoriaDelito);

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarDelito @pTC_Nombre, @pTC_Descripcion, @pTN_IdCategoriaDelito",
                    nombreParam, descripcionParam, categoriaDelitoParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al insertar el delito.");
                }

                return delito;
            }
            catch (SqlException ex)
            {
                // Si el error proviene de SQL Server, se captura el mensaje del procedimiento almacenado
                throw new Exception($"Error en la base de datos al insertar delito: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar el delito: {ex.Message}", ex);
            }
        }

        public async Task<List<Delito>> obtenerDelitos()
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar delitos
                var delitosDA = await _context.TSOLITEL_DelitoDA
                    .FromSqlRaw("EXEC PA_ConsultarDelito")
                    .ToListAsync();

                // Mapeo de los resultados a la entidad Delito
                var delitos = delitosDA.Select(da => new Delito
                {
                    TN_IdDelito = da.TN_IdDelito,
                    TC_Nombre = da.TC_Nombre,
                    TC_Descripcion = da.TC_Descripcion,
                    TN_IdCategoriaDelito = da.TN_IdCategoriaDelito
                }).ToList();

                return delitos;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener delitos: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener la lista de delitos: {ex.Message}", ex);
            }
        }
    }
}
