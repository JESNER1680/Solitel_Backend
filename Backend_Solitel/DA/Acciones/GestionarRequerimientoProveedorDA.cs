using BC.Modelos;
using BC.Reglas_de_Negocio;
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
    public class GestionarRequerimientoProveedorDA : IGestionarRequerimientoProveedorDA
    {
        private readonly SolitelContext _context;

        public GestionarRequerimientoProveedorDA(SolitelContext _context)
        {
            this._context = _context;
        }
        public async Task<bool> InsertarRequerimientoProveedor(RequerimientoProveedor requerimientoProveedor)
        {
            try
            {
                // Definir los parámetros para el procedimiento almacenado
                var fechaInicioParam = new SqlParameter("@PF_FechaInicio", requerimientoProveedor.TF_FechaInicio);
                var fechaFinalParam = new SqlParameter("@PF_FechaFinal", requerimientoProveedor.TF_FechaFinal);
                var requerimientoParam = new SqlParameter("@PC_Requerimiento", requerimientoProveedor.TC_Requerimiento);
                var idSolicitudProveedorParam = new SqlParameter("@PN_IdSolicitudProveedor", requerimientoProveedor.TN_NumeroSolicitud);

                var idRequerimientoInsertadoParam = new SqlParameter("@IdRequerimientoInsertado", System.Data.SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output // Parámetro de salida
                };

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsetarRequerimientoProveedor @PF_FechaInicio, @PF_FechaFinal, @PC_Requerimiento, @PN_IdSolicitudProveedor, @IdRequerimientoInsertado OUTPUT",
                    fechaInicioParam, fechaFinalParam, requerimientoParam, idSolicitudProveedorParam, idRequerimientoInsertadoParam);

                

                int idRequerimientoInsertado = (int)idRequerimientoInsertadoParam.Value;

                //Insertar los tipos de solicitud para el requerimiento
                foreach (TipoSolicitud tipoSolicitud in requerimientoProveedor.tipoSolicitudes)
                {
                    var idTipoSolicitudParam = new SqlParameter("@PN_IdTipoSolicitud", tipoSolicitud.TN_IdTipoSolicitud);
                    var idRequerimientoParam = new SqlParameter("@PN_IdRequerimientoProveedor", idRequerimientoInsertado);

                    await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarTipoSolicitudARequerimientoProveedor @PN_IdTipoSolicitud, @PN_IdRequerimientoProveedor",
                    idTipoSolicitudParam, idRequerimientoParam);
                }

                //Insertar los datos requeridos para el requerimiento
                foreach (DatoRequerido datoRequerido in requerimientoProveedor.datosRequeridos)
                {
                    var datoRequeridoParam = new SqlParameter("@PC_DatoRequerido", datoRequerido.TC_DatoRequerido);
                    var motivacionParam = new SqlParameter("@PC_Motivacion", datoRequerido.TC_Motivacion);
                    var tipoDatoParam = new SqlParameter("@PN_TipoDato", datoRequerido.TN_IdTipoDato);
                    var idRequerimientoParam = new SqlParameter("@PN_IdRequerimiento", idRequerimientoInsertado);

                    await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarDatoRequerido @PC_DatoRequerido, @PC_Motivacion, @PN_TipoDato, @PN_IdRequerimiento",
                    datoRequeridoParam, motivacionParam, tipoDatoParam, idRequerimientoParam);
                }

                var resultadoRequerimiento = await _context.SaveChangesAsync();
                //var resultadoDatosRequerimiento = await _context.SaveChangesAsync();


                if (resultadoRequerimiento < 0)
                {
                    throw new Exception("Error al insertar el requerimiento.");
                }

                return resultadoRequerimiento >= 0 ? true : false;
            }
            catch (SqlException ex)
            {
                // Si el error proviene de SQL Server, se captura el mensaje del procedimiento almacenado
                throw new Exception($"Error en la base de datos al insertar el requerimiento: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar el requerimiento: {ex.Message}", ex);
            }
        }
    }
}
