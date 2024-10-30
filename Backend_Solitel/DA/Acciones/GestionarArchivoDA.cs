using BC.Modelos;
using BC.Reglas_de_Negocio;
using BW.Interfaces.DA;
using DA.Contexto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DA.Acciones
{
    public class GestionarArchivoDA : IGestionarArchivoDA
    {
        private readonly SolitelContext _context;

        public GestionarArchivoDA(SolitelContext solitelContext)
        {
            this._context = solitelContext;
        }
        public async Task<bool> InsertarArchivo_RequerimientoProveedor(Archivo archivo, int idRequerimientoProveedor)
        {
            try
            {
                // Definir los parámetros para el procedimiento almacenado
                var nombreArchivoParam = new SqlParameter("@PC_NombreArchivo", archivo.TC_Nombre);
                var hashArchivoParam = new SqlParameter("@PC_HashArchivo", archivo.TC_HashAchivo);
                var contenidoParam = new SqlParameter("@PV_Contenido", archivo.TV_Contenido);
                var formatoArchivoParam = new SqlParameter("@PC_FormatoArchivo", archivo.TC_FormatoAchivo);
                var fechaModificacionParam = new SqlParameter("@PF_FechaModificacion", archivo.TF_FechaModificacion);
                var idRequerimientoProveedorParam = new SqlParameter("@PN_IdRequerimientoProveedor", idRequerimientoProveedor);

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarArchivo_RequerimientoProveedor @PC_NombreArchivo, @PC_HashArchivo, @PV_Contenido, " +
                    "@PC_FormatoArchivo, @PF_FechaModificacion, @PN_IdRequerimientoProveedor",
                    nombreArchivoParam, hashArchivoParam, contenidoParam, formatoArchivoParam, fechaModificacionParam, idRequerimientoProveedorParam
                );

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al insertar el archivo de requerimientoProveedor.");
                }

                return resultado >= 0 ? true : false;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al insertar el archivo de requerimientoProveedor: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar el archivo de requerimientoProveedor: {ex.Message}", ex);
            }
        }

        public async Task<Archivo> ObtenerArchivoPorIdAsync(int idArchivo)
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar por ID
                var archivoDA = await _context.TSOLITEL_ArchivoDA
                    .FromSqlRaw("EXEC PA_ConsultarArchivoPorID @PN_IdArchivo = {0}", idArchivo)
                    .ToListAsync();  // Convertir la consulta a una lista

                var primerArchivo = archivoDA.FirstOrDefault();  // Obtener el primer elemento si existe

                if (primerArchivo == null)
                {
                    throw new Exception($"No se encontró un archivo con el ID {idArchivo}.");
                }

                // Mapear los resultados a la entidad Proveedor (si es necesario)
                var archivo = new Archivo
                {
                    TN_IdArchivo = primerArchivo.TN_IdArchivo,
                    TC_Nombre = primerArchivo.TC_Nombre,
                    TC_HashAchivo = primerArchivo.TC_HashArchivo,
                    TC_FormatoAchivo = primerArchivo.TC_FormatoArchivo,
                    TV_Contenido = primerArchivo.TV_Contenido,
                    TF_FechaModificacion = primerArchivo.TF_FechaModificacion
                };

                return archivo;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener el archivo con ID {idArchivo}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener el archivo con ID {idArchivo}: {ex.Message}", ex);
            }
        }

        public Task<List<Archivo>> ObtenerArchivosDeSolicitudesProveedor(List<int> idsSolicitudesProveedor)
        {
            throw new NotImplementedException();
        }
    }
}
