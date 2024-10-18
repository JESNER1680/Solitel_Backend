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
    public class GestionarSolicitudProveedorDA : IGestionarSolicitudProveedorDA
    {
        private readonly SolitelContext _context;

        public GestionarSolicitudProveedorDA(SolitelContext context)
        {
            _context = context;
        }

        public async Task<int> InsertarSolicitudProveedor(SolicitudProveedor solicitudProveedor)
        {
            try
            {
                //Definir los parámetros para el procedimiento almacenado
                var numeroUnicoParam = new SqlParameter("@PN_NumeroUnico", solicitudProveedor.TN_NumeroUnico);
                var numeroCasoParam = new SqlParameter("@PN_NumeroCaso", solicitudProveedor.TN_NumeroCaso);
                var imputadoParam = new SqlParameter("@PC_Imputado", solicitudProveedor.TC_Imputado);
                var ofendidoParam = new SqlParameter("@PC_Ofendido", solicitudProveedor.TC_Ofendido);
                var resenniaParam = new SqlParameter("@PC_Resennia", solicitudProveedor.TC_Resennia);
                var diasTransacurridosParam = new SqlParameter("@PN_DiasTranscurridos", solicitudProveedor.TN_DiasTranscurridos);
                var urgenteParam = new SqlParameter("@PB_Urgente", solicitudProveedor.TB_Urgente);
                var aprobadoParam = new SqlParameter("@PB_Aprobado", solicitudProveedor.TB_Aprobado);
                var fechaCreacionParam = new SqlParameter("@PF_FechaCreacion", solicitudProveedor.TF_FechaCrecion);
                var fechaModificacionParam = new SqlParameter("@PF_FechaModificacion", solicitudProveedor.TF_FechaModificacion);
                var idUsuarioCreadorParam = new SqlParameter("@PN_IdUsuarioCreador", solicitudProveedor.TN_IdUsuarioCreador);
                var idDelitoParam = new SqlParameter("@PN_IdDelito", solicitudProveedor.TN_IdDelito);
                var idCategoriaDelitoParam = new SqlParameter("@PN_IdCategoriaDelito", solicitudProveedor.TN_IdCategoriaDelito);
                var idModalidadParam = new SqlParameter("@PN_IdModalidad", solicitudProveedor.TN_IdModalida);
                var idSubModalidadParam = new SqlParameter("@PN_IdSubModalidad", solicitudProveedor.TN_IdSubModalidad);
                var idEstadoParam = new SqlParameter("@PN_IdEstado", solicitudProveedor.TN_IdEstado);
                var idProveedorParam = new SqlParameter("@PN_IdProveedor", solicitudProveedor.TN_IdProveedor);
                var idFiscaliaParam = new SqlParameter("@PN_IdFiscalia", solicitudProveedor.TN_IdFiscalia);
                var idOficinaParam = new SqlParameter("@PN_IdOficina", solicitudProveedor.TN_IdOficina);

                var idSolicitudParam = new SqlParameter("@IdSolicitudInsertada", System.Data.SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output // Parámetro de salida
                };


                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarSolicitudProveedor @PN_NumeroUnico, @PN_NumeroCaso, @PC_Imputado, @PC_Ofendido, @PC_Resennia," +
                    " @PN_DiasTranscurridos, @PB_Urgente, @PB_Aprobado, @PF_FechaCreacion, @PF_FechaModificacion, @PN_IdUsuarioCreador," +
                    " @PN_IdDelito, @PN_IdCategoriaDelito, @PN_IdModalidad, @PN_IdSubModalidad, @PN_IdEstado, @PN_IdProveedor, @PN_IdFiscalia," +
                    " @PN_IdOficina, @IdSolicitudInsertada OUTPUT",
                    numeroUnicoParam, numeroCasoParam, imputadoParam, ofendidoParam, resenniaParam, diasTransacurridosParam, urgenteParam,
                    aprobadoParam, fechaCreacionParam, fechaModificacionParam, idUsuarioCreadorParam, idDelitoParam, idCategoriaDelitoParam,
                    idModalidadParam, idSubModalidadParam, idEstadoParam, idProveedorParam, idFiscaliaParam, idOficinaParam, idSolicitudParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al insertar la solicitud para proveedor.");
                }

                int idInsertado = (int)idSolicitudParam.Value;

                return idInsertado;

            }
            catch (SqlException ex)
            {
                // Si el error proviene de SQL Server, se captura el mensaje del procedimiento almacenado
                throw new Exception($"Error en la base de datos al insertar la solicitud para proveedor: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar la solicitud para proveedor: {ex.Message}", ex);
            }
        }

        public async Task<List<SolicitudProveedor>> obtenerSolicitudesProveedor()
        {
            
            return null;
        }
    }
}
