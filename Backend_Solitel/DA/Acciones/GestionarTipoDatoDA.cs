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
    public class GestionarTipoDatoDA: IGestionarTipoDatoDA
    {
        private readonly SolitelContext _context;

        public GestionarTipoDatoDA(SolitelContext context)
        {
            _context = context;
        }

        public async Task<TipoDato> insertarTipoDato(TipoDato tipoDato)
        {
            try
            {
                // Definir los parámetros para el procedimiento almacenado
                var nombreParam = new SqlParameter("@pTC_Nombre", tipoDato.Nombre);
                var descripcionParam = new SqlParameter("@pTC_Descripcion", tipoDato.Descripcion);

                // Definir el parámetro de salida para capturar el ID generado
                var idParam = new SqlParameter("@pTN_IdTipoDato", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarTipoDato @pTN_IdTipoDato OUTPUT, @pTC_Nombre, @pTC_Descripcion",
                    idParam, nombreParam, descripcionParam
                );

                // Capturar el ID generado desde el parámetro de salida
                var nuevoId = (int)idParam.Value;

                if (nuevoId <= 0)
                {
                    throw new Exception("Error al obtener el ID del tipo de dato recién insertado.");
                }

                // Asignar el ID generado a la entidad TipoDato
                tipoDato.IdTipoDato = nuevoId;

                return tipoDato;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al insertar el tipo de dato: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar el tipo de dato: {ex.Message}", ex);
            }
        }

        public async Task<List<TipoDato>> obtenerTipoDato()
        {
            try
            {
                var tiposDatoDA = await _context.TSOLITEL_TipoDatoDA
                    .FromSqlRaw("EXEC PA_ConsultarTipoDato")
                    .ToListAsync();

                var tiposDato = tiposDatoDA.Select(da => new TipoDato
                {
                    IdTipoDato = da.TN_IdTipoDato,
                    Nombre = da.TC_Nombre,
                    Descripcion = da.TC_Descripcion,
                }).ToList();

                return tiposDato;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error en la base de datos al obtener los tipos de dato: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error inesperado al obtener los tipos de dato: {ex.Message}", ex);
            }
        }

        public async Task<bool> eliminarTipoDato(int id)
        {
            try
            {
                var idDatoParam = new SqlParameter("@pTN_IdTipoDato", id);

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_EliminarTipoDato @pTN_IdTipoDato", idDatoParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al eliminar el tipo de dato.");
                }

                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error en la base de datos al eliminar el tipo de dato: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error inesperado al eliminar el tipo de dato: {ex.Message}", ex);
            }
        }

        public async Task<TipoDato> obtenerTipoDato(int id)
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar un tipo de dato por ID
                var tipoDatoDA = await _context.TSOLITEL_TipoDatoDA
                    .FromSqlRaw("EXEC PA_ConsultarTipoDato @pTN_IdTipoDato = {0}", id)
                    .ToListAsync();  // Obtener el resultado como lista

                var tipoDato = tipoDatoDA.FirstOrDefault();  // Obtener el primer elemento si hay uno

                // Verificar si se encontró el tipo de dato
                if (tipoDato == null)
                {
                    throw new Exception($"No se encontró un tipo de dato con el ID {id}.");
                }

                // Mapear el resultado a la entidad TipoDato
                var tipoDatoResult = new TipoDato
                {
                    IdTipoDato = tipoDato.TN_IdTipoDato,
                    Nombre = tipoDato.TC_Nombre,
                    Descripcion = tipoDato.TC_Descripcion
                };

                return tipoDatoResult;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener el tipo de dato con ID {id}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener el tipo de dato con ID {id}: {ex.Message}", ex);
            }
        }
    }
}
