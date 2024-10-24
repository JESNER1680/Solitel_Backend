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

        public async Task<List<DatoRequerido>> ConsultarDatosRequeridos(int idRequerimientoProveedor)
        {
            try
            {
                var idRequerimientoProveedorParam = new SqlParameter("@PN_IdRequerimientoProveedor", idRequerimientoProveedor);

                // Ejecutar el procedimiento almacenado
                var datosRequeridosDA = await _context.TSOLITEL_DatoRequeridoDA
                    .FromSqlRaw("EXEC PA_ConsultarDatosRequeridos @PN_IdRequerimientoProveedor", idRequerimientoProveedorParam)
                    .ToListAsync();

                // Mapeo de los resultados
                var datosRequeridos = datosRequeridosDA.Select(da => new DatoRequerido
                {
                    TN_IdDatoRequerido = da.TN_IdDatoRequerido,
                    TC_DatoRequerido = da.TC_DatoRequerido,
                    TC_Motivacion = da.TC_Motivacion,
                    TN_IdTipoDato = da.TN_IdTipoDato

                }).ToList();

                return datosRequeridos;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener solicitudProveedor: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener la lista de solicitudesProveedor: {ex.Message}", ex);
            }
        }

        public async Task<List<RequerimientoProveedor>> ConsultarRequerimientosProveedor(int idSolicitudProveedor)
        {
            try
            {
                var idSolicitudProveedorParam = new SqlParameter("@PN_IdSolicitudProveedor", idSolicitudProveedor);

                // Ejecutar el procedimiento almacenado
                var requerimientosProveedorDA = await _context.TSOLITEL_RequerimientoProveedorDA
                    .FromSqlRaw("EXEC PA_ConsultarRequerimientosProveedor @PN_IdSolicitudProveedor", idSolicitudProveedorParam)
                    .ToListAsync();

                // Mapeo de los resultados
                var requerimientosProveedor = requerimientosProveedorDA.Select(da => new RequerimientoProveedor
                {
                    TN_IdRequerimientoProveedor = da.TN_IdRequerimientoProveedor,
                    TF_FechaFinal = da.TF_FechaFinal,
                    TF_FechaInicio = da.TF_FechaInicio,
                    TC_Requerimiento = da.TC_Requerimiento,
                    TN_NumeroSolicitud = 0,
                    datosRequeridos = new List<DatoRequerido>(),
                    tipoSolicitudes = new List<TipoSolicitud>()

                }).ToList();

                return requerimientosProveedor;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener solicitudProveedor: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener la lista de solicitudesProveedor: {ex.Message}", ex);
            }
        }

        public async Task<List<TipoSolicitud>> ConsultarTipoSolicitudes(int idRequerimientoProveedor)
        {
            try
            {
                var idRequerimientoProveedorParam = new SqlParameter("@PN_IdRequerimientoProveedor", idRequerimientoProveedor);

                // Ejecutar el procedimiento almacenado
                var tipoSolicitudesDA = await _context.TSOLITEL_TipoSolicitudDA
                    .FromSqlRaw("EXEC PA_ConsultarTipoSolicitudes @PN_IdRequerimientoProveedor", idRequerimientoProveedorParam)
                    .ToListAsync();

                // Mapeo de los resultados
                var tipoSolicitudes = tipoSolicitudesDA.Select(da => new TipoSolicitud
                {
                    TN_IdTipoSolicitud = da.TN_IdTipoSolicitud,
                    TC_Nombre = da.TC_Nombre,
                    TC_Descripcion = da.TC_Descripcion

                }).ToList();

                return tipoSolicitudes;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener solicitudProveedor: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener la lista de solicitudesProveedor: {ex.Message}", ex);
            }
        }

        public async Task<int> InsertarRequerimientoProveedor(RequerimientoProveedor requerimientoProveedor)
        {
            try
            {
                // Definir los parámetros para el procedimiento almacenado
                var fechaInicioParam = new SqlParameter("@PF_FechaInicio", requerimientoProveedor.TF_FechaInicio);
                var fechaFinalParam = new SqlParameter("@PF_FechaFinal", requerimientoProveedor.TF_FechaFinal);
                var requerimientoParam = new SqlParameter("@PC_Requerimiento", requerimientoProveedor.TC_Requerimiento);

                var idRequerimientoInsertadoParam = new SqlParameter("@IdRequerimientoInsertado", System.Data.SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output // Parámetro de salida
                };

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarRequerimientoProveedor @PF_FechaInicio, @PF_FechaFinal, @PC_Requerimiento, @IdRequerimientoInsertado OUTPUT",
                    fechaInicioParam, fechaFinalParam, requerimientoParam, idRequerimientoInsertadoParam);

                

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


                if (resultadoRequerimiento < 0)
                {
                    throw new Exception("Error al insertar el requerimiento.");
                }

                return idRequerimientoInsertado;
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
