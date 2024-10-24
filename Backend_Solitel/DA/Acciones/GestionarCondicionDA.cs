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
    public class GestionarCondicionDA: IGestionarCondicionDA
    {
        private readonly SolitelContext _context;

        public GestionarCondicionDA(SolitelContext context)
        {
            _context = context;
        }

        public async Task<Condicion> insertarCondicion(Condicion condicion)
        {
            try
            {
                // Definir los parámetros de entrada y salida para el procedimiento almacenado
                var nombreParam = new SqlParameter("@pTC_Nombre", condicion.TC_Nombre);
                var descripcionParam = new SqlParameter("@pTC_Descripcion", condicion.TC_Descripcion);

                // Definir el parámetro de salida para capturar el ID generado
                var idParam = new SqlParameter("@pTN_IdCondicion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutar el procedimiento almacenado con los parámetros definidos
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarCondicion @pTN_IdCondicion OUTPUT, @pTC_Nombre, @pTC_Descripcion",
                    idParam, nombreParam, descripcionParam
                );

                // Verificar si se generó un ID válido
                var nuevoId = (int)idParam.Value;
                if (nuevoId <= 0)
                {
                    throw new Exception("Error al obtener el ID de la condición recién insertada.");
                }

                // Asignar el ID generado a la entidad de condición
                condicion.TN_IdCondicion = nuevoId;

                // Devolver el objeto con el ID asignado
                return condicion;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al insertar la condición: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar la condición: {ex.Message}", ex);
            }
        }

        public async Task<List<Condicion>> obtenerCondicionesTodas()
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar condiciones
                var condicionesDA = await _context.TSOLITEL_CondicionDA
                    .FromSqlRaw("EXEC PA_ConsultarCondicion")
                    .ToListAsync();

                // Mapeo de los resultados a la entidad Condicion
                var condiciones = condicionesDA.Select(da => new Condicion
                {
                    TN_IdCondicion = da.TN_IdCondicion,
                    TC_Nombre = da.TC_Nombre,
                    TC_Descripcion = da.TC_Descripcion,
                }).ToList();

                return condiciones;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener las condiciones: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener las condiciones: {ex.Message}", ex);
            }
        }

        public async Task<bool> eliminarCondicion(int id)
        {
            try
            {
                // Definir el parámetro para el procedimiento almacenado
                var idCondicionParam = new SqlParameter("@pTN_IdCondicion", id);

                // Ejecutar el procedimiento almacenado para eliminar (lógicamente)
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_EliminarCondicion @pTN_IdCondicion", idCondicionParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al eliminar la condición.");
                }

                // Retornar un objeto Condicion con el Id de la condición eliminada
                return true;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al eliminar la condición: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al eliminar la condición: {ex.Message}", ex);
            }
        }

        public async Task<Condicion> obtenerCondicionId(int id)
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar una condición por ID
                var condicionDA = await _context.TSOLITEL_CondicionDA
                    .FromSqlRaw("EXEC PA_ConsultarCondicion @pTN_IdCondicion = {0}", id)
                    .ToListAsync();  // Usar ToListAsync y luego FirstOrDefault() para obtener un único resultado

                var condicion = condicionDA.FirstOrDefault();  // Obtener el primer elemento si hay

                // Verificar si se encontró la condición
                if (condicion == null)
                {
                    throw new Exception($"No se encontró una condición con el ID {id}.");
                }

                // Mapear los resultados a la entidad Condicion
                var condicionResult = new Condicion
                {
                    TN_IdCondicion = condicion.TN_IdCondicion,
                    TC_Nombre = condicion.TC_Nombre,
                    TC_Descripcion = condicion.TC_Descripcion
                };

                return condicionResult;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener la condición con ID {id}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener la condición con ID {id}: {ex.Message}", ex);
            }
        }
    }
}
