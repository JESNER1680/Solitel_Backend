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
    public class GestionarSolicitudAnalistaDA : IGestionarSolicitudAnalistaDA
    {
        private readonly SolitelContext solitelContext;
        public GestionarSolicitudAnalistaDA(SolitelContext _solitelContext)
        {
            this.solitelContext = _solitelContext;
        }
        public async Task<bool> CrearSolicitudAnalista(SolicitudAnalisis solicitudAnalisis)
        {
            try
            {
                // Parámetros para el SP PA_InsertarSolicitudAnalisis
                var fechaDelHechoParam = new SqlParameter("@TF_FechaDelHecho", solicitudAnalisis.TF_FechaDelHecho);
                var otrosDetallesParam = new SqlParameter("@TC_OtrosDetalles", solicitudAnalisis.TC_OtrosDetalles);
                var otrosObjetivosParam = new SqlParameter("@TC_OtrosObjetivosDeAnalisis", (object)solicitudAnalisis.TC_OtrosObjetivosDeAnalisis ?? DBNull.Value);
                var aprobadoParam = new SqlParameter("@TB_Aprobado", solicitudAnalisis.TB_Aprobado);
                var fechaCreacionParam = new SqlParameter("@TF_FechaCrecion", (object)solicitudAnalisis.TF_FechaCrecion ?? DBNull.Value);
                var numeroSolicitudParam = new SqlParameter("@TN_NumeroSolicitud", solicitudAnalisis.TN_NumeroSolicitud);
                var idOficinaParam = new SqlParameter("@TN_IdOficina", solicitudAnalisis.TN_IdOficina);

                // Parámetro de salida para capturar el IdAnalisis generado
                var idAnalisisParam = new SqlParameter("@TN_IdSolicitudAnalisis", System.Data.SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output
                };

                // Ejecutar el procedimiento almacenado PA_InsertarSolicitudAnalisis
                await solitelContext.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarSolicitudAnalisis @TF_FechaDelHecho, @TC_OtrosDetalles, @TC_OtrosObjetivosDeAnalisis, @TB_Aprobado, @TF_FechaCrecion, @TN_NumeroSolicitud, @TN_IdOficina, @TN_IdSolicitudAnalisis OUTPUT",
                    fechaDelHechoParam, otrosDetallesParam, otrosObjetivosParam, aprobadoParam, fechaCreacionParam, numeroSolicitudParam, idOficinaParam, idAnalisisParam);

                // Capturamos el IdAnalisis generado
                var idAnalisis = (int)idAnalisisParam.Value;

                // Insertar objetivos de análisis relacionados si existen
                if (solicitudAnalisis.ObjetivosAnalisis != null && solicitudAnalisis.ObjetivosAnalisis.Count > 0)
                {
                    foreach (var objetivoAnalisis in solicitudAnalisis.ObjetivosAnalisis)
                    {
                        var nombreParam = new SqlParameter("@TC_Nombre", objetivoAnalisis.TC_Nombre);
                        var descripcionParam = new SqlParameter("@TC_Descripcion", objetivoAnalisis.TC_Descripcion);
                        var borradoParam = new SqlParameter("@TB_Borrado", 0);

                        // Ejecutar el procedimiento almacenado PA_InsertarObjetivoAnalisis
                        await solitelContext.Database.ExecuteSqlRawAsync(
                            "EXEC PA_InsertarObjetivoAnalisis @TC_Nombre, @TC_Descripcion, @TB_Borrado",
                            nombreParam, descripcionParam, borradoParam);

                        // Insertar en la tabla intermedia PA_ObjetivoAnalisis_SolicitudAnalisis
                        var idObjetivoParam = new SqlParameter("@TN_IdObjetivo", objetivoAnalisis.TN_IdObjetivoAnalisis);
                        var idAnalisisIntermedioParam = new SqlParameter("@TN_IdAnalisis", idAnalisis);

                        await solitelContext.Database.ExecuteSqlRawAsync(
                            "EXEC PA_ObjetivoAnalisis_SolicitudAnalisis @TN_IdObjetivo, @TN_IdAnalisis",
                            idObjetivoParam, idAnalisisIntermedioParam);
                    }
                }

                // Insertar requerimientos de análisis asociados si existen
                if (solicitudAnalisis.Requerimentos != null && solicitudAnalisis.Requerimentos.Count > 0)
                {
                    foreach (var requerimento in solicitudAnalisis.Requerimentos)
                    {
                        var objetivoParam = new SqlParameter("@TC_Objetivo", requerimento.TC_Objetivo);
                        var utilizadoPorParam = new SqlParameter("@TC_UtilizadoPor", requerimento.TC_UtilizadoPor);
                        var idTipoParam = new SqlParameter("@TN_IdTipo", requerimento.TN_IdTipo);
                        var idAnalisisParamReq = new SqlParameter("@TN_IdAnalisis", idAnalisis);

                        await solitelContext.Database.ExecuteSqlRawAsync(
                            "EXEC PA_InsertarRequerimentoAnalisis @TC_Objetivo, @TC_UtilizadoPor, @TN_IdTipo, @TN_IdAnalisis",
                            objetivoParam, utilizadoPorParam, idTipoParam, idAnalisisParamReq);
                    }
                }

                // Insertar archivos relacionados a la solicitud de análisis si existen
                if (solicitudAnalisis.Archivos != null && solicitudAnalisis.Archivos.Count > 0)
                {
                    foreach (var archivo in solicitudAnalisis.Archivos)
                    {
                        var idAnalisisArchivoParam = new SqlParameter("@pIdAnalisis", idAnalisis);
                        var idArchivoParam = new SqlParameter("@pIdArchivo", archivo.TN_IdArchivo);

                        await solitelContext.Database.ExecuteSqlRawAsync(
                            "EXEC PA_InsertarSolicitudAnalisis_Archivo @pIdAnalisis, @pIdArchivo",
                            idAnalisisArchivoParam, idArchivoParam);
                    }
                }

                // Insertar tipos de análisis asociados a la solicitud si existen
                if (solicitudAnalisis.TiposAnalisis != null && solicitudAnalisis.TiposAnalisis.Count > 0)
                {
                    foreach (var tipoAnalisis in solicitudAnalisis.TiposAnalisis)
                    {
                        var idTipoAnalisisParam = new SqlParameter("@pIdTipoAnalisis", tipoAnalisis.TN_IdTipoAnalisis);
                        var idAnalisisTipoParam = new SqlParameter("@pIdAnalisis", idAnalisis);

                        // Ejecutar el procedimiento almacenado PA_InsertarTipoAnalisis_SolicitudAnalisis
                        await solitelContext.Database.ExecuteSqlRawAsync(
                            "EXEC PA_InsertarTipoAnalisis_SolicitudAnalisis @pIdTipoAnalisis, @pIdAnalisis",
                            idTipoAnalisisParam, idAnalisisTipoParam);
                    }
                }

                // Guardar cambios en la base de datos
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
