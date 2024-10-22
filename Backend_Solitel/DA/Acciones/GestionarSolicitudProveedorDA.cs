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

        public async Task<List<SolicitudProveedor>> consultarSolicitudesProveedorPorNumeroUnico(int numeroUnico)
        {
            try
            {
                var numeroUnicoParam = new SqlParameter("@PN_NumeroUnico", numeroUnico);

                // Ejecutar el procedimiento almacenado
                var solicitudesProveedorDA = await _context.TSOLITEL_SolicitudProveedorDA
                    .FromSqlRaw("EXEC PA_ConsultarSolicitudesProveedorPorNumeroUnico @PN_NumeroUnico", numeroUnicoParam)
                    .ToListAsync();

                // Mapeo de los resultados
                var solicitudesProveedor = solicitudesProveedorDA.Select(da => new SolicitudProveedor
                {
                    IdSolicitudProveedor = da.TN_IdSolicitudProveedor,
                    NumeroUnico = da.TN_NumeroUnico,
                    NumeroCaso = da.TN_NumeroCaso,
                    Imputado = da.TC_Imputado,
                    Ofendido = da.TC_Ofendido,
                    Resennia = da.TC_Resennia,
                    Urgente = da.TB_Urgente,
                    Aprobado = da.TB_Aprobado,
                    FechaCrecion = da.TF_FechaCrecion,
                    FechaModificacion = da.TF_FechaModificacion,
                    Proveedor = new Proveedor { TN_IdProveedor = da.TN_IdProveedor, TC_Nombre = da.TC_NombreProveedor },
                    Delito = new Delito { TN_IdDelito = da.TN_IdDelito, TN_IdCategoriaDelito = da.TN_IdCategoriaDelito, TC_Nombre = da.TC_NombreDelito },
                    CategoriaDelito = new CategoriaDelito { TC_Nombre = da.TC_NombreCategoriaDelito, TN_IdCategoriaDelito = da.TN_IdCategoriaDelito },
                    DiasTranscurridos = da.TN_DiasTranscurridos,
                    Estado = new Estado { TN_IdEstado = da.TN_IdEstado, TC_Nombre = da.TC_NombreEstado },
                    Fiscalia = new Fiscalia { TN_IdFiscalia = da.TN_IdFiscalia, TC_Nombre = da.TC_NombreFiscalia },
                    Modalidad = new Modalidad { TN_IdModalidad = da.TN_IdModalidad, TC_Nombre = da.TC_NombreModalidad },
                    Oficina = new Oficina { TN_IdOficina = da.TN_IdOficina, TC_Nombre = da.TC_NombreOficina },
                    SubModalidad = new SubModalidad { TN_IdSubModalidad = da.TN_IdSubModalidad, TC_Nombre = da.TC_NombreSubModalidad, TN_IdModalida = da.TN_IdModalidad },
                    UsuarioCreador = new Usuario { TN_IdUsuario = da.TN_IdUsuarioCreador, TC_Nombre = da.TC_NombreUsuarioCreador }

                }).ToList();

                return solicitudesProveedor;
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

        public async Task<int> InsertarSolicitudProveedor(SolicitudProveedor solicitudProveedor)
        {
            try
            {
                //Definir los parámetros para el procedimiento almacenado
                var numeroUnicoParam = new SqlParameter("@PN_NumeroUnico", solicitudProveedor.NumeroUnico);
                var numeroCasoParam = new SqlParameter("@PN_NumeroCaso", solicitudProveedor.NumeroCaso);
                var imputadoParam = new SqlParameter("@PC_Imputado", solicitudProveedor.Imputado);
                var ofendidoParam = new SqlParameter("@PC_Ofendido", solicitudProveedor.Ofendido);
                var resenniaParam = new SqlParameter("@PC_Resennia", solicitudProveedor.Resennia);
                var diasTransacurridosParam = new SqlParameter("@PN_DiasTranscurridos", solicitudProveedor.DiasTranscurridos);
                var urgenteParam = new SqlParameter("@PB_Urgente", solicitudProveedor.Urgente);
                var aprobadoParam = new SqlParameter("@PB_Aprobado", solicitudProveedor.Aprobado);
                var fechaCreacionParam = new SqlParameter("@PF_FechaCreacion", solicitudProveedor.FechaCrecion);
                var fechaModificacionParam = new SqlParameter("@PF_FechaModificacion", solicitudProveedor.FechaModificacion);
                var idUsuarioCreadorParam = new SqlParameter("@PN_IdUsuarioCreador", solicitudProveedor.UsuarioCreador.TN_IdUsuario);
                var idDelitoParam = new SqlParameter("@PN_IdDelito", solicitudProveedor.Delito.TN_IdDelito);
                var idCategoriaDelitoParam = new SqlParameter("@PN_IdCategoriaDelito", solicitudProveedor.CategoriaDelito.TN_IdCategoriaDelito);
                var idModalidadParam = new SqlParameter("@PN_IdModalidad", solicitudProveedor.Modalidad.TN_IdModalidad);
                var idSubModalidadParam = new SqlParameter("@PN_IdSubModalidad", solicitudProveedor.SubModalidad.TN_IdSubModalidad);
                var idEstadoParam = new SqlParameter("@PN_IdEstado", solicitudProveedor.Estado.TN_IdEstado);
                var idProveedorParam = new SqlParameter("@PN_IdProveedor", solicitudProveedor.Proveedor.TN_IdProveedor);
                var idFiscaliaParam = new SqlParameter("@PN_IdFiscalia", solicitudProveedor.Fiscalia.TN_IdFiscalia);
                var idOficinaParam = new SqlParameter("@PN_IdOficina", solicitudProveedor.Oficina.TN_IdOficina);

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

        public async Task<List<int>> ListarNumerosUnicosTramitados()
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar
                var numerosUnicosTramitados = await _context.Database
                        .SqlQuery<int>($"EXEC PA_ListarNumerosUnicosTramitados")
                        .ToListAsync();


                return numerosUnicosTramitados;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener los numeros unicos tramitados: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener los numeros unicos tramitados: {ex.Message}", ex);
            }
        }

        public async Task<List<SolicitudProveedor>> obtenerSolicitudesProveedor(int pageNumber, int pageSize)
        {

            try
            {
                var pageNumberParam = new SqlParameter("@PageNumber", pageNumber);
                var pageSizeParam = new SqlParameter("@PageSize", pageSize);

                // Ejecutar el procedimiento almacenado
                var solicitudesProveedorDA = await _context.TSOLITEL_SolicitudProveedorDA
                    .FromSqlRaw("EXEC PA_ConsultarSolicitudesProveedor @PageNumber, @PageSize", pageNumberParam, pageSizeParam)
                    .ToListAsync();

                // Mapeo de los resultados
                var solicitudesProveedor = solicitudesProveedorDA.Select(da => new SolicitudProveedor
                {
                    IdSolicitudProveedor = da.TN_IdSolicitudProveedor,
                    NumeroUnico = da.TN_NumeroUnico,
                    NumeroCaso = da.TN_NumeroCaso,
                    Imputado = da.TC_Imputado,
                    Ofendido = da.TC_Ofendido,
                    Resennia = da.TC_Resennia,
                    Urgente = da.TB_Urgente,
                    Aprobado = da.TB_Aprobado,
                    FechaCrecion = da.TF_FechaCrecion,
                    FechaModificacion = da.TF_FechaModificacion,
                    Proveedor = new Proveedor { TN_IdProveedor = da.TN_IdProveedor, TC_Nombre = da.TC_NombreProveedor },
                    Delito = new Delito { TN_IdDelito = da.TN_IdDelito, TN_IdCategoriaDelito = da.TN_IdCategoriaDelito, TC_Nombre = da.TC_NombreDelito },
                    CategoriaDelito = new CategoriaDelito { TC_Nombre = da.TC_NombreCategoriaDelito , TN_IdCategoriaDelito = da.TN_IdCategoriaDelito},
                    DiasTranscurridos = da.TN_DiasTranscurridos,
                    Estado = new Estado { TN_IdEstado = da.TN_IdEstado, TC_Nombre = da.TC_NombreEstado },
                    Fiscalia = new Fiscalia { TN_IdFiscalia = da.TN_IdFiscalia, TC_Nombre = da.TC_NombreFiscalia},
                    Modalidad = new Modalidad { TN_IdModalidad = da.TN_IdModalidad, TC_Nombre = da.TC_NombreModalidad },
                    Oficina = new Oficina { TN_IdOficina = da.TN_IdOficina, TC_Nombre = da.TC_NombreOficina},
                    SubModalidad = new SubModalidad { TN_IdSubModalidad = da.TN_IdSubModalidad, TC_Nombre = da.TC_NombreSubModalidad, TN_IdModalida = da.TN_IdModalidad },
                    UsuarioCreador = new Usuario { TN_IdUsuario = da.TN_IdUsuarioCreador, TC_Nombre = da.TC_NombreUsuarioCreador }

                }).ToList();

                return solicitudesProveedor;
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
    }
}
