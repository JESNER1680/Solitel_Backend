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
    public class GestionarSolicitudAnalistaDA : IGestionarSolicitudAnalistaDA
    {
        private readonly SolitelContext solitelContext;
        public GestionarSolicitudAnalistaDA(SolitelContext _solitelContext)
        {
            this.solitelContext = _solitelContext;
        }

        public async Task<List<SolicitudAnalisis>> ConsultarSolicitudesAnalisisAsync(
        int pageNumber,
        int pageSize,
        int? idEstado = null,
        string numeroUnico = null,
        DateTime? fechaInicio = null,
        DateTime? fechaFin = null,
        string caracterIngresado = null)
        {
            try
            {
                // Definir parámetros del procedimiento almacenado
                var pageNumberParam = new SqlParameter("@pPageNumber", pageNumber);
                var pageSizeParam = new SqlParameter("@pPageSize", pageSize);
                var idEstadoParam = new SqlParameter("@pIdEstado", idEstado ?? (object)DBNull.Value);
                var numeroUnicoParam = new SqlParameter("@pNumeroUnico", numeroUnico ?? (object)DBNull.Value);
                var fechaInicioParam = new SqlParameter("@pFechaInicio", fechaInicio ?? (object)DBNull.Value);
                var fechaFinParam = new SqlParameter("@pFechaFin", fechaFin ?? (object)DBNull.Value);
                var caracterIngresadoParam = new SqlParameter("@pCaracterIngresado", caracterIngresado ?? (object)DBNull.Value);

                // Ejecutar el procedimiento almacenado y obtener los resultados
                var solicitudesAnalisisDA = await this.solitelContext.TSOLITEL_SolicitudAnalisisDA
                    .FromSqlRaw(
                        "EXEC PA_ConsultarSolicitudesAnalisis @pPageNumber, @pPageSize, @pIdEstado, @pNumeroUnico, @pFechaInicio, @pFechaFin, @pCaracterIngresado",
                        pageNumberParam, pageSizeParam, idEstadoParam, numeroUnicoParam, fechaInicioParam, fechaFinParam, caracterIngresadoParam)
                    .ToListAsync();

                // Mapear los resultados a la clase `SolicitudAnalisis`
                var solicitudesAnalisis = solicitudesAnalisisDA.Select(da => new SolicitudAnalisis
                {
                    IdSolicitudAnalisis = da.TN_IdAnalisis,
                    FechaDelHecho = da.TF_FechaDeHecho,
                    OtrosDetalles = da.TC_OtrosDetalles,
                    OtrosObjetivosDeAnalisis = da.TC_OtrosObjetivosDeAnalisis,
                    Aprobado = da.TB_Aprobado,
                    FechaCrecion = da.TF_FechaDeCreacion,
                    SolicitudesProveedor = da.SolicitudProveedor.Select(sp => new SolicitudProveedor
                    {
                        IdSolicitudProveedor = sp.TN_IdSolicitud,
                        NumeroUnico = sp.TN_NumeroUnico,
                        NumeroCaso = sp.TN_NumeroCaso,
                        Imputado = sp.TC_Imputado,
                        Ofendido = sp.TC_Ofendido,
                        Resennia = sp.TC_Resennia,
                        Urgente = sp.TB_Urgente,
                        Aprobado = sp.TB_Aprobado,
                        FechaCrecion = sp.TF_FechaDeCreacion,
                        Proveedor = new Proveedor
                        {
                            IdProveedor = sp.TN_IdProveedor,
                            Nombre = sp.TC_NombreProveedor
                        },
                        Delito = new Delito
                        {
                            IdDelito = sp.TN_IdDelito,
                            IdCategoriaDelito = sp.TN_IdCategoriaDelito,
                            Nombre = sp.TC_NombreDelito
                        },
                        CategoriaDelito = new CategoriaDelito
                        {
                            IdCategoriaDelito = sp.TN_IdCategoriaDelito,
                            Nombre = sp.TC_NombreCategoriaDelito
                        },
                        Estado = new Estado
                        {
                            IdEstado = sp.TN_IdEstado,
                            Nombre = sp.TC_NombreEstado
                        },
                        Fiscalia = new Fiscalia
                        {
                            IdFiscalia = sp.TN_IdFiscalia,
                            Nombre = sp.TC_NombreFiscalia
                        },
                        Modalidad = new Modalidad
                        {
                            IdModalidad = sp.TN_IdModalidad ?? 0,
                            Nombre = sp.TC_NombreModalidad
                        },
                        SubModalidad = new SubModalidad
                        {
                            IdSubModalidad = sp.TN_IdSubModalidad ?? 0,
                            Nombre = sp.TC_NombreSubModalidad
                        }
                    }).ToList()
                }).ToList();

                return solicitudesAnalisis;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al consultar solicitudes de análisis: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al consultar solicitudes de análisis: {ex.Message}", ex);
            }
        }

        public async Task<bool> CrearSolicitudAnalista(SolicitudAnalisis solicitudAnalisis)
        {
            Console.WriteLine(solicitudAnalisis.Archivos.Count);
            Console.WriteLine(solicitudAnalisis.ObjetivosAnalisis.Count);
            Console.WriteLine(solicitudAnalisis.Condiciones.Count);
            Console.WriteLine(solicitudAnalisis.Requerimentos.Count);

            try
            {
                // Parámetros para PA_InsertarSolicitudAnalisis
                var fechaDeHechoParam = new SqlParameter("@TF_FechaDeHecho", solicitudAnalisis.FechaDelHecho);
                var otrosDetallesParam = new SqlParameter("@TC_OtrosDetalles", solicitudAnalisis.OtrosDetalles);
                var otrosObjetivosParam = new SqlParameter("@TC_OtrosObjetivosDeAnalisis", (object)solicitudAnalisis.OtrosObjetivosDeAnalisis ?? DBNull.Value);
                var aprobadoParam = new SqlParameter("@TB_Aprobado", solicitudAnalisis.Aprobado);
                var fechaCreacionParam = new SqlParameter("@TF_FechaCrecion", (object)solicitudAnalisis.FechaCrecion ?? DBNull.Value);
                var numeroSolicitudParam = new SqlParameter("@TN_NumeroSolicitud", solicitudAnalisis.NumeroSolicitud);
                var idOficinaParam = new SqlParameter("@TN_IdOficina", solicitudAnalisis.IdOficina);

                // Parámetro de salida para capturar el ID de análisis generado
                var idAnalisisParam = new SqlParameter("@TN_IdSolicitudAnalisis", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutar PA_InsertarSolicitudAnalisis para crear la solicitud de análisis
                await solitelContext.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarSolicitudAnalisis @TF_FechaDeHecho, @TC_OtrosDetalles, @TC_OtrosObjetivosDeAnalisis, @TB_Aprobado, @TF_FechaCrecion, @TN_NumeroSolicitud, @TN_IdOficina, @TN_IdSolicitudAnalisis OUTPUT",
                    fechaDeHechoParam, otrosDetallesParam, otrosObjetivosParam, aprobadoParam, fechaCreacionParam, numeroSolicitudParam, idOficinaParam, idAnalisisParam);

                // Obtener el ID generado para el análisis
                var idAnalisis = (int)idAnalisisParam.Value;

                // Insertar objetivos de análisis asociados, si existen
                if (solicitudAnalisis.ObjetivosAnalisis != null && solicitudAnalisis.ObjetivosAnalisis.Count > 0)
                {
                    foreach (var objetivoAnalisis in solicitudAnalisis.ObjetivosAnalisis)
                    {
                        var idObjetivoParam = new SqlParameter("@TN_IdObjetivo", objetivoAnalisis.IdObjetivoAnalisis);
                        var idAnalisisIntermedioParam = new SqlParameter("@TN_IdAnalisis", idAnalisis);

                        await solitelContext.Database.ExecuteSqlRawAsync(
                            "EXEC PA_InsertarObjetivoAnalisis_SolicitudAnalisis @TN_IdObjetivo, @TN_IdAnalisis",
                            idObjetivoParam, idAnalisisIntermedioParam);
                    }
                }

                // Insertar requerimientos asociados, si existen
                if (solicitudAnalisis.Requerimentos != null && solicitudAnalisis.Requerimentos.Count > 0)
                {
                    foreach (var requerimiento in solicitudAnalisis.Requerimentos)
                    {
                        var tipoExiste = await solitelContext.TSOLITEL_TipoDatoDA
                            .AnyAsync(t => t.TN_IdTipoDato == requerimiento.IdTipo);

                        var idTipo = tipoExiste ? requerimiento.IdTipo : 6;

                        await solitelContext.Database.ExecuteSqlRawAsync(
                            "EXEC PA_InsertarRequerimentoAnalisis @TC_Objetivo, @TC_UtilizadoPor, @TN_IdTipo, @TN_IdAnalisis",
                            new SqlParameter("@TC_Objetivo", requerimiento.Objetivo),
                            new SqlParameter("@TC_UtilizadoPor", requerimiento.UtilizadoPor),
                            new SqlParameter("@TN_IdTipo", idTipo),
                            new SqlParameter("@TN_IdAnalisis", idAnalisis));
                    }
                }

                // Insertar archivos asociados, si existen
                if (solicitudAnalisis.Archivos != null && solicitudAnalisis.Archivos.Count > 0)
                {
                    foreach (var archivo in solicitudAnalisis.Archivos)
                    {
                        await solitelContext.Database.ExecuteSqlRawAsync(
                            "EXEC PA_InsertarSolicitudAnalisis_Archivo @pIdAnalisis, @pIdArchivo, @pTipo",
                            new SqlParameter("@pIdAnalisis", idAnalisis),
                            new SqlParameter("@pIdArchivo", archivo.IdArchivo),
                            new SqlParameter("@pTipo", archivo.FormatoArchivo ?? "DefaultTipo") // Valor predeterminado si es null
                        );
                    }
                }

                // Insertar tipos de análisis asociados, si existen
                if (solicitudAnalisis.TiposAnalisis != null && solicitudAnalisis.TiposAnalisis.Count > 0)
                {
                    foreach (var tipoAnalisis in solicitudAnalisis.TiposAnalisis)
                    {
                        await solitelContext.Database.ExecuteSqlRawAsync(
                            "EXEC PA_InsertarTipoAnalisis_SolicitudAnalisis @pIdTipoAnalisis, @pIdAnalisis",
                            new SqlParameter("@pIdTipoAnalisis", tipoAnalisis.IdTipoAnalisis),
                            new SqlParameter("@pIdAnalisis", idAnalisis));
                    }
                }

                // Insertar condiciones asociadas a la solicitud, si existen
                if (solicitudAnalisis.Condiciones != null && solicitudAnalisis.Condiciones.Count > 0)
                {
                    foreach (var condicion in solicitudAnalisis.Condiciones)
                    {
                        await solitelContext.Database.ExecuteSqlRawAsync(
                            "EXEC PA_InsertarSolicitudAnalisis_Condicion @TN_IdAnalisis, @TN_IdCondicion",
                            new SqlParameter("@TN_IdAnalisis", idAnalisis),
                            new SqlParameter("@TN_IdCondicion", condicion.IdCondicion));
                    }
                }

                // Insertar proveedores asociados a la solicitud, si existen
                if (solicitudAnalisis.SolicitudesProveedor != null && solicitudAnalisis.SolicitudesProveedor.Count > 0)
                {
                    foreach (var solicitudProveedor in solicitudAnalisis.SolicitudesProveedor)
                    {
                        await solitelContext.Database.ExecuteSqlRawAsync(
                            "EXEC PA_InsertarSolicitudAnalisis_SolicitudProveedor @TN_IdAnalisis, @TN_IdSolicitud",
                            new SqlParameter("@TN_IdAnalisis", idAnalisis),
                            new SqlParameter("@TN_IdSolicitud", solicitudProveedor.IdSolicitudProveedor)
                        );
                    }
                }

                await solitelContext.SaveChangesAsync();
                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error en la base de datos al insertar la solicitud de análisis: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error inesperado al insertar la solicitud de análisis: {ex.Message}", ex);
            }
        }



    }
}
