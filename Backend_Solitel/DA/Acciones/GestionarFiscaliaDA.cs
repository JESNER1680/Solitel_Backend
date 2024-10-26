using BC.Modelos;
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
    public class GestionarFiscaliaDA: IGestionarFiscaliaDA
    {
        private readonly SolitelContext _context;

        public GestionarFiscaliaDA(SolitelContext context)
        {
            _context = context;
        }

        public async Task<bool> eliminarFiscalia(int id)
        {
            try
            {
                // Definir el parámetro para el procedimiento almacenado
                var idFiscaliaParam = new SqlParameter("@pTN_IdFiscalia", id);

                // Ejecutar el procedimiento almacenado para eliminar lógicamente
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_EliminarFiscalia @pTN_IdFiscalia", idFiscaliaParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al eliminar la fiscalía.");
                }

                // Retornar un objeto Fiscalia con el Id de la fiscalía eliminada
                return true;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al eliminar fiscalía: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al eliminar la fiscalía: {ex.Message}", ex);
            }
        }

        public async Task<Fiscalia> insertarFiscalia(string nombre)
        {
            try
            {
                // Definir el parámetro de entrada para el nombre
                var nombreParam = new SqlParameter("@pTC_Nombre", nombre);

                // Definir el parámetro de salida para capturar el ID generado
                var idParam = new SqlParameter("@pTN_IdFiscalia", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutar el procedimiento almacenado con los parámetros
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarFiscalia @pTN_IdFiscalia OUTPUT, @pTC_Nombre",
                    idParam, nombreParam
                );

                // Capturar el ID generado desde el parámetro de salida
                var nuevoId = (int)idParam.Value;
                if (nuevoId <= 0)
                {
                    throw new Exception("Error al obtener el ID de la fiscalía recién insertada.");
                }

                // Crear una instancia de Fiscalia con el ID y nombre
                var fiscalia = new Fiscalia
                {
                    TN_IdFiscalia = nuevoId,
                    TC_Nombre = nombre
                };

                return fiscalia;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al insertar fiscalía: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar la fiscalía: {ex.Message}", ex);
            }
        }

        public async Task<List<Fiscalia>> obtenerFiscaliasTodas()
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar todas las fiscalías
                var fiscaliasDA = await _context.TSOLITEL_FiscaliaDA
                    .FromSqlRaw("EXEC PA_ConsultarFiscalia")
                    .ToListAsync();

                // Mapear los resultados a la entidad Fiscalia
                var fiscalias = fiscaliasDA.Select(da => new Fiscalia
                {
                    TN_IdFiscalia = da.TN_IdFiscalia,
                    TC_Nombre = da.TC_Nombre
                }).ToList();

                return fiscalias;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener fiscalías: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener la lista de fiscalías: {ex.Message}", ex);
            }
        }

        public async Task<Fiscalia> obtenerFiscaliaId(int id)
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar una fiscalía por ID
                var fiscaliaDA = await _context.TSOLITEL_FiscaliaDA
                    .FromSqlRaw("EXEC PA_ConsultarFiscalia @pTN_IdFiscalia = {0}", id)
                    .ToListAsync();  // Obtener el resultado como lista

                var fiscalia = fiscaliaDA.FirstOrDefault();  // Obtener el primer elemento si existe

                // Verificar si se encontró la fiscalía
                if (fiscalia == null)
                {
                    throw new Exception($"No se encontró una fiscalía con el ID {id}.");
                }

                // Mapear los resultados a la entidad Fiscalia
                var fiscaliaEntidad = new Fiscalia
                {
                    TN_IdFiscalia = fiscalia.TN_IdFiscalia,
                    TC_Nombre = fiscalia.TC_Nombre
                };

                return fiscaliaEntidad;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener la fiscalía con ID {id}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener la fiscalía con ID {id}: {ex.Message}", ex);
            }
        }

    }
}
