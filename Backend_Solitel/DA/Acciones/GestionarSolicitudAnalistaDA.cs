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
        public async Task<bool> CrearSolicitudAnalista(SolicitudAnalisis solicitudAnalisis)
        {
            Console.WriteLine(solicitudAnalisis.Archivos.Count);
            Console.WriteLine(solicitudAnalisis.ObjetivosAnalisis.Count);
            Console.WriteLine(solicitudAnalisis.Condiciones.Count);
            Console.WriteLine(solicitudAnalisis.Requerimentos.Count);

            try
            {
                // Parámetros para PA_InsertarSolicitudAnalisis
                var fechaDelHechoParam = new SqlParameter("@TF_FechaDelHecho", solicitudAnalisis.FechaDelHecho);
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
                    "EXEC PA_InsertarSolicitudAnalisis @TF_FechaDelHecho, @TC_OtrosDetalles, @TC_OtrosObjetivosDeAnalisis, @TB_Aprobado, @TF_FechaCrecion, @TN_NumeroSolicitud, @TN_IdOficina, @TN_IdSolicitudAnalisis OUTPUT",
                    fechaDelHechoParam, otrosDetallesParam, otrosObjetivosParam, aprobadoParam, fechaCreacionParam, numeroSolicitudParam, idOficinaParam, idAnalisisParam);

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
