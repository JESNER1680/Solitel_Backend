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
                var nombreArchivoParam = new SqlParameter("@PC_NombreArchivo", archivo.Nombre);
                var contenidoParam = new SqlParameter("@PV_Contenido", archivo.Contenido);
                var formatoArchivoParam = new SqlParameter("@PC_FormatoArchivo", archivo.FormatoArchivo);
                var fechaModificacionParam = new SqlParameter("@PF_FechaModificacion", archivo.FechaModificacion);
                var idRequerimientoProveedorParam = new SqlParameter("@PN_IdRequerimientoProveedor", idRequerimientoProveedor);

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarArchivo_RequerimientoProveedor @PC_NombreArchivo, @PV_Contenido, " +
                    "@PC_FormatoArchivo, @PF_FechaModificacion, @PN_IdRequerimientoProveedor",
                    nombreArchivoParam, contenidoParam, formatoArchivoParam, fechaModificacionParam, idRequerimientoProveedorParam
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
                    IdArchivo = primerArchivo.TN_IdArchivo,
                    Nombre = primerArchivo.TC_Nombre,
                    FormatoArchivo = primerArchivo.TC_FormatoArchivo,
                    Contenido = primerArchivo.TV_Contenido,
                    FechaModificacion = primerArchivo.TF_FechaModificacion
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

        public async Task<List<Archivo>> ObtenerArchivosDeSolicitudesProveedor(List<int> idsSolicitudesProveedor)
        {
            var archivos = new List<Archivo>();

            try
            {
                foreach (var idSolicitudProveedor in idsSolicitudesProveedor)
                {
                    var idSolicitudParam = new SqlParameter("@PN_IdSolicitudProveedor", idSolicitudProveedor);

                    // Ejecutar el procedimiento almacenado
                    var archivosDA = await _context.TSOLITEL_ArchivoDA
                        .FromSqlRaw("EXEC PA_ConsultarArchivosDeSolicitudProveedor @PN_IdSolicitudProveedor", idSolicitudParam)
                        .ToListAsync();

                    // Mapear resultados a la entidad de negocio Archivo
                    archivos.AddRange(archivosDA.Select(da => new Archivo
                    {
                        IdArchivo = da.TN_IdArchivo,
                        Nombre = da.TC_Nombre,
                        Contenido = da.TV_Contenido ?? new byte[0], // Asegúrate de que 'TV_Contenido' está en la entidad si es necesario
                        FormatoArchivo = da.TC_FormatoArchivo,
                        FechaModificacion = da.TF_FechaModificacion // O un valor específico si la fecha no está en el resultado
                    }));
                }

                return archivos;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error en la base de datos al obtener archivos de solicitudes proveedor: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error inesperado al obtener archivos de solicitudes proveedor: {ex.Message}", ex);
            }
        }


    }
}
