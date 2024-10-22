using BC.Modelos;
using BW.Interfaces.DA;
using DA.Contexto;
using DA.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<Fiscalia> eliminarFiscalia(int id)
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
                return new Fiscalia { TN_IdFiscalia = id };
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

        public async Task<bool> insertarFiscalia(string nombre)
        {
            try
            {
                // Definir el parámetro para el procedimiento almacenado
                var nombreParam = new SqlParameter("@pTC_Nombre", nombre);

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarFiscalia @pTC_Nombre", nombreParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al insertar la fiscalía.");
                }

                return true;  // Retornar true si la inserción fue exitosa
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

        public async Task<List<Fiscalia>> obtenerFiscalias()
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

        public async Task<Fiscalia> obtenerFiscalia(int id)
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
