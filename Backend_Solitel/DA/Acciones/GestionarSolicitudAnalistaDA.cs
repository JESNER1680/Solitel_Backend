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
        public GestionarSolicitudAnalistaDA(SolitelContext _solitelContext) {
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

                // Ejecutar el procedimiento almacenado PA_InsertarSolicitudAnalisis
                await solitelContext.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarSolicitudAnalisis @TF_FechaDelHecho, @TC_OtrosDetalles, @TC_OtrosObjetivosDeAnalisis, @TB_Aprobado, @TF_FechaCrecion, @TN_NumeroSolicitud, @TN_IdOficina",
                    fechaDelHechoParam, otrosDetallesParam, otrosObjetivosParam, aprobadoParam, fechaCreacionParam, numeroSolicitudParam, idOficinaParam);

                // Obtener el ID del análisis recién insertado
                var idAnalisis = solicitudAnalisis.TN_IdSolicitudAnalisis; // Asumimos que esto lo obtienes luego de insertar

                // Inserta los objetivos de análisis relacionados
                if (solicitudAnalisis.requerimentos != null && solicitudAnalisis.requerimentos.Count > 0)
                {
                    foreach (var objetivoAnalisis in solicitudAnalisis.requerimentos)
                    {
                        // Parámetros para el SP PA_InsertarObjetivoAnalisis
                        var nombreParam = new SqlParameter("@TC_Nombre", objetivoAnalisis.TC_Objetivo);
                        var descripcionParam = new SqlParameter("@TC_Descripcion", objetivoAnalisis.TC_UtilizadoPor);
                        var borradoParam = new SqlParameter("@TB_Borrado", 0);

                        // Ejecutar el procedimiento almacenado PA_InsertarObjetivoAnalisis
                        await solitelContext.Database.ExecuteSqlRawAsync(
                            "EXEC PA_InsertarObjetivoAnalisis @TC_Nombre, @TC_Descripcion, @TB_Borrado",
                            nombreParam, descripcionParam, borradoParam);

                        // Ahora insertar en la tabla intermedia PA_ObjetivoAnalisis_SolicitudAnalisis
                        var idObjetivo = objetivoAnalisis.TN_IdRequerimientoAnalisis; // Aquí obtienes el ID del objetivo recién insertado

                        var idObjetivoParam = new SqlParameter("@TN_IdObjetivo", idObjetivo);
                        var idAnalisisParam = new SqlParameter("@TN_IdAnalisis", idAnalisis);

                        // Ejecutar el procedimiento almacenado PA_ObjetivoAnalisis_SolicitudAnalisis
                        await solitelContext.Database.ExecuteSqlRawAsync(
                            "EXEC PA_ObjetivoAnalisis_SolicitudAnalisis @TN_IdObjetivo, @TN_IdAnalisis",
                            idObjetivoParam, idAnalisisParam);
                    }
                }

                // Requerimientos de análisis asociados (si existen)
                if (solicitudAnalisis.requerimentos != null && solicitudAnalisis.requerimentos.Count > 0)
                {
                    foreach (var requerimento in solicitudAnalisis.requerimentos)
                    {
                        var objetivoParam = new SqlParameter("@TC_Objetivo", requerimento.TC_Objetivo);
                        var utilizadoPorParam = new SqlParameter("@TC_UtilizadoPor", requerimento.TC_UtilizadoPor);
                        var idTipoParam = new SqlParameter("@TN_IdTipo", requerimento.TN_IdTipo);
                        var idAnalisisParamReq = new SqlParameter("@TN_IdAnalisis", idAnalisis);

                        // Ejecutar el procedimiento almacenado PA_InsertarRequerimentoAnalisis
                        await solitelContext.Database.ExecuteSqlRawAsync(
                            "EXEC PA_InsertarRequerimentoAnalisis @TC_Objetivo, @TC_UtilizadoPor, @TN_IdTipo, @TN_IdAnalisis",
                            objetivoParam, utilizadoPorParam, idTipoParam, idAnalisisParamReq);
                    }
                }

                // Guardar cambios en la base de datos
                await solitelContext.SaveChangesAsync();
                return true;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al insertar la solicitud de análisis: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar la solicitud de análisis: {ex.Message}", ex);
            }
        }
    }
}
