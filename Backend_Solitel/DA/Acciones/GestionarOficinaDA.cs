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
    public class GestionarOficinaDA : IGestionarOficinaDA
    {
        private readonly SolitelContext _context;

        public GestionarOficinaDA(SolitelContext context)
        {
            _context = context;
        }

        public async Task<List<Oficina>> ConsultarOficinas()
        {
            try
            {
                // Ejecutar el procedimiento almacenado
                var oficinasDA = await _context.TSOLITEL_OficinaDA
                    .FromSqlRaw("EXEC PA_ConsultarOficina")
                    .ToListAsync();

                // Mapeo de los resultados
                var oficinas = oficinasDA.Select(da => new Oficina
                {
                    TN_IdOficina = da.TN_IdOficina,
                    TC_Nombre = da.TC_Nombre

                }).ToList();

                return oficinas;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener oficinas: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener la lista de oficinas: {ex.Message}", ex);
            }
        }

        public async Task<bool> EliminarOficina(int idOficina)
        {
            try
            {
                // Definir el parámetro para el procedimiento almacenado
                var idOficinaParam = new SqlParameter("@PN_IdOficina", idOficina);

                // Ejecutar el procedimiento almacenado para eliminar (lógicamente)
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_EliminarOficina @PN_IdOficina", idOficinaParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al eliminar la oficina.");
                }

                return resultado >= 0 ? true : false;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al eliminar la oficina: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al eliminar la oficina: {ex.Message}", ex);
            }
        }

        public async Task<bool> InsertarOficina(Oficina oficina)
        {
            try
            {
                // Definir los parámetros para el procedimiento almacenado
                var nombreParam = new SqlParameter("@PC_Nombre", oficina.TC_Nombre);

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarOficina @PC_Nombre",
                    nombreParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al insertar la oficina.");
                }

                return resultado >= 0 ? true : false;
            }
            catch (SqlException ex)
            {
                // Si el error proviene de SQL Server, se captura el mensaje del procedimiento almacenado
                throw new Exception($"Error en la base de datos al insertar la oficina: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar la oficina: {ex.Message}", ex);
            }
        }
    }
}
