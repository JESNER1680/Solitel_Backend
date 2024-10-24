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
    public class GestionarCategoriaDelitoDA : IGestionarCategoriaDelitoDA
    {
        private readonly SolitelContext _context;

        public GestionarCategoriaDelitoDA(SolitelContext context)
        {
            _context = context;
        }

        public async Task<bool> eliminarCategoriaDelito(int id)
        {
            try
            {
                // Definir el parámetro para el procedimiento almacenado
                var idCategoriaParam = new SqlParameter("@pTN_IdCategoriaDelito", id);

                // Ejecutar el procedimiento almacenado para eliminar (lógicamente)
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_EliminarCategoriaDelito @pTN_IdCategoriaDelito", idCategoriaParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al eliminar la categoría de delito.");
                }

                // Retornar un objeto CategoriaDelito con el Id de la categoría eliminada
                return true;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al eliminar la categoría de delito: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al eliminar la categoría de delito: {ex.Message}", ex);
            }
        }

        public async Task<CategoriaDelito> insertarCategoriaDelito(CategoriaDelito categoriaDelito)
        {
            try
            {
                // Definir los parámetros de entrada y salida para el procedimiento almacenado
                var nombreParam = new SqlParameter("@pTC_Nombre", categoriaDelito.TC_Nombre);
                var descripcionParam = new SqlParameter("@pTC_Descripcion", categoriaDelito.TC_Descripcion);

                // Definir el parámetro de salida para capturar el ID generado
                var idParam = new SqlParameter("@pTN_IdCategoriaDelito", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutar el procedimiento almacenado con los parámetros definidos
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarCategoriaDelito @pTN_IdCategoriaDelito OUTPUT, @pTC_Nombre, @pTC_Descripcion",
                    idParam, nombreParam, descripcionParam
                );

                // Verificar si se generó un ID válido
                var nuevoId = (int)idParam.Value;
                if (nuevoId <= 0)
                {
                    throw new Exception("Error al obtener el ID de la categoría de delito recién insertada.");
                }

                // Asignar el ID generado a la entidad de categoría de delito
                categoriaDelito.TN_IdCategoriaDelito = nuevoId;

                // Devolver el objeto con el ID asignado
                return categoriaDelito;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al insertar la categoría de delito: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar la categoría de delito: {ex.Message}", ex);
            }
        }

        public async Task<List<CategoriaDelito>> obtenerCategoriaDelito()
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar categorías de delito
                var categoriaDelitosDA = await _context.TSOLITEL_CategoriaDelitoDA
                    .FromSqlRaw("EXEC PA_ConsultarCategoriaDelito")
                    .ToListAsync();

                // Mapeo de los resultados a la entidad CategoriaDelito
                var categorias = categoriaDelitosDA.Select(da => new CategoriaDelito
                {
                    TN_IdCategoriaDelito = da.TN_IdCategoriaDelito,
                    TC_Nombre = da.TC_Nombre,
                    TC_Descripcion = da.TC_Descripcion,
                }).ToList();

                return categorias;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener categorías de delito: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener la lista de categorías de delito: {ex.Message}", ex);
            }
        }

        public async Task<CategoriaDelito> obtenerCategoriaDelito(int id)
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar una categoría de delito por ID
                var categoriaDelitoDA = await _context.TSOLITEL_CategoriaDelitoDA
                    .FromSqlRaw("EXEC PA_ConsultarCategoriaDelito @pTN_IdCategoriaDelito = {0}", id)
                    .ToListAsync(); // Usar FirstOrDefaultAsync para obtener un único resultado

                var categoriaDelito = categoriaDelitoDA.FirstOrDefault();  // Obtener el primer elemento si hay

                // Verificar si se encontró la categoría
                if (categoriaDelito == null)
                {
                    throw new Exception($"No se encontró una categoría de delito con el ID {id}.");
                }

                // Mapear los resultados a la entidad CategoriaDelito
                var categoria = new CategoriaDelito
                {
                    TN_IdCategoriaDelito = categoriaDelito.TN_IdCategoriaDelito,
                    TC_Nombre = categoriaDelito.TC_Nombre,
                    TC_Descripcion = categoriaDelito.TC_Descripcion
                };

                return categoria;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener la categoría de delito con ID {id}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener la categoría de delito con ID {id}: {ex.Message}", ex);
            }
        }
    }
}
