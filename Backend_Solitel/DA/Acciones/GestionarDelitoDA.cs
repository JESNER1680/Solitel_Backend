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
    public class GestionarDelitoDA: IGestionarDelitoDA
    {
        private readonly SolitelContext _context;

        public GestionarDelitoDA(SolitelContext context)
        {
            _context = context;
        }

        public async Task<bool> eliminarDelito(int id)
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
                return true;
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
                // Definir los parámetros de entrada para el procedimiento almacenado
                var nombreParam = new SqlParameter("@pTC_Nombre", delito.TC_Nombre);
                var descripcionParam = new SqlParameter("@pTC_Descripcion", delito.TC_Descripcion);
                var categoriaDelitoParam = new SqlParameter("@pTN_IdCategoriaDelito", delito.TN_IdCategoriaDelito);

                // Definir el parámetro de salida para capturar el ID generado
                var idParam = new SqlParameter("@pTN_IdDelito", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutar el procedimiento almacenado con los parámetros
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarDelito @pTN_IdDelito OUTPUT, @pTC_Nombre, @pTC_Descripcion, @pTN_IdCategoriaDelito",
                    idParam, nombreParam, descripcionParam, categoriaDelitoParam
                );

                // Capturar el ID generado desde el parámetro de salida
                var nuevoId = (int)idParam.Value;
                if (nuevoId <= 0)
                {
                    throw new Exception("Error al obtener el ID del delito recién insertado.");
                }

                // Asignar el ID generado a la entidad de delito
                delito.TN_IdDelito = nuevoId;

                // Devolver el objeto con el ID asignado
                return delito;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al insertar delito: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar el delito: {ex.Message}", ex);
            }
        }

        public async Task<List<Delito>> obtenerDelitosTodos()
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

        public async Task<Delito> obtenerDelitoId(int id)
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar un delito por ID
                var delitoDA = await _context.TSOLITEL_DelitoDA
                    .FromSqlRaw("EXEC PA_ConsultarDelito @pTN_IdDelito = {0}", id)
                    .ToListAsync(); // Usar ToListAsync para convertir la consulta en una lista

                var delito = delitoDA.FirstOrDefault();  // Obtener el primer elemento si existe

                // Verificar si se encontró el delito
                if (delito == null)
                {
                    throw new Exception($"No se encontró un delito con el ID {id}.");
                }

                // Mapear los resultados a la entidad Delito
                var delitoEntidad = new Delito
                {
                    TN_IdDelito = delito.TN_IdDelito,
                    TC_Nombre = delito.TC_Nombre,
                    TC_Descripcion = delito.TC_Descripcion,
                    TN_IdCategoriaDelito = delito.TN_IdCategoriaDelito
                };

                return delitoEntidad;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener el delito con ID {id}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener el delito con ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<List<Delito>> obtenerDelitosPorCategoria(int id)
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar delitos por categoría
                var delitosDA = await _context.TSOLITEL_DelitoDA
                    .FromSqlRaw("EXEC PA_ConsultarDelitosPorCategoria @pTN_IdCategoriaDelito = {0}", id)
                    .ToListAsync();  // Obtener la lista de delitos

                // Mapear los resultados a la entidad Delito
                var delitos = delitosDA.Select(da => new Delito
                {
                    TN_IdDelito = da.TN_IdDelito,
                    TC_Nombre = da.TC_Nombre,
                    TC_Descripcion = da.TC_Descripcion,
                    TN_IdCategoriaDelito = da.TN_IdCategoriaDelito,
                }).ToList();

                return delitos;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener los delitos de la categoría con ID {id}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener los delitos de la categoría con ID {id}: {ex.Message}", ex);
            }
        }
    }
}
