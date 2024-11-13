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

        public async Task<bool> AprobarSolicitudProveedor(int idSolicitudProveedor, int idUsuario, string? observacion)
        {
            try
            {
                //Definir los parámetros para el procedimiento almacenado
                var idSolicitudProveedorParam = new SqlParameter("@PN_IdSolicitudProveedor", idSolicitudProveedor);
                var idUsuarioParam = new SqlParameter("@PN_IdUsuario", idUsuario);
                var observacionParam = new SqlParameter("PC_Observacion", observacion)
                {
                    Size = 255,
                    Value = (object)observacion ?? DBNull.Value // Manejar nulos
                };


                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_AprobarSolicitudProveedor @PN_IdSolicitudProveedor, @PN_IdUsuario, @PC_Observacion",
                    idSolicitudProveedorParam, idUsuarioParam, observacionParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al insertar al aprobar la solicitud.");
                }


                return resultado >= 0 ? true : false;

            }
            catch (SqlException ex)
            {
                // Si el error proviene de SQL Server, se captura el mensaje del procedimiento almacenado
                throw new Exception($"Error en la base de datos al aprobar la solicitud: {ex.Message}", ex);
            }
        }

        public async Task<bool> ActualizarEstadoFinalizado(int id, int idUsuario, string observacion = null)
        {
            try
            {
                // Define los parámetros para el procedimiento almacenado
                var idSolicitudParam = new SqlParameter("@pTN_IdSolicitud", id);
                var idUsuarioParam = new SqlParameter("@PN_IdUsuario", idUsuario);
                var observacionParam = new SqlParameter("@PC_Observacion", observacion ?? (object)DBNull.Value);

                // Ejecuta el procedimiento almacenado
                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_ActualizarEstadoFinalizadoSolicitudProveedor @pTN_IdSolicitud, @PN_IdUsuario, @PC_Observacion",
                    idSolicitudParam, idUsuarioParam, observacionParam
                );

                // Verifica si el resultado fue exitoso
                return result < 0;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al actualizar el estado de la solicitud: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al actualizar el estado de la solicitud: {ex.Message}", ex);
            }
        }

        public async Task<bool> ActualizarEstadoLegajo(int id, int idUsuario, string observacion = null)
        {
            try
            {
                // Define los parámetros para el procedimiento almacenado
                var idSolicitudParam = new SqlParameter("@pTN_IdSolicitud", id);
                var idUsuarioParam = new SqlParameter("@PN_IdUsuario", idUsuario);
                var observacionParam = new SqlParameter("@PC_Observacion", observacion ?? (object)DBNull.Value);

                // Ejecuta el procedimiento almacenado
                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_ActualizarEstadoLegajoSolicitudProveedor @pTN_IdSolicitud, @PN_IdUsuario, @PC_Observacion",
                    idSolicitudParam, idUsuarioParam, observacionParam
                );

                // Verifica si el resultado fue exitoso
                return result < 0;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al actualizar el estado a Legajo de la solicitud: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al actualizar el estado a Legajo de la solicitud: {ex.Message}", ex);
            }
        }

        public async Task<List<SolicitudProveedor>> consultarSolicitudesProveedorPorNumeroUnico(string numeroUnico)
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
                    IdSolicitudProveedor = da.TN_IdSolicitud,
                    NumeroUnico = da.TC_NumeroUnico,
                    NumeroCaso = da.TC_NumeroCaso,
                    Imputado = da.TC_Imputado,
                    Ofendido = da.TC_Ofendido,
                    Resennia = da.TC_Resennia,
                    Urgente = da.TB_Urgente,
                    Aprobado = da.TB_Aprobado,
                    FechaCrecion = da.TF_FechaDeCreacion,
                    UsuarioCreador = new Usuario
                    {
                        IdUsuario = da.TN_IdUsuario,
                        Nombre = da.TC_NombreUsuario
                    },
                    Proveedor = new Proveedor
                    {
                        IdProveedor = da.TN_IdProveedor,
                        Nombre = da.TC_NombreProveedor
                    },
                    Delito = new Delito
                    {
                        IdDelito = da.TN_IdDelito,
                        Nombre = da.TC_NombreDelito
                    },
                    CategoriaDelito = new CategoriaDelito
                    {
                        IdCategoriaDelito = da.TN_IdCategoriaDelito,
                        Nombre = da.TC_NombreCategoriaDelito
                    },
                    Estado = new Estado
                    {
                        IdEstado = da.TN_IdEstado,
                        Nombre = da.TC_NombreEstado
                    },
                    Fiscalia = new Fiscalia
                    {
                        IdFiscalia = da.TN_IdFiscalia,
                        Nombre = da.TC_NombreFiscalia
                    },
                    Modalidad = da.TN_IdModalidad.HasValue ? new Modalidad
                    {
                        IdModalidad = da.TN_IdModalidad.Value,
                        Nombre = da.TC_NombreModalidad
                    } : null,
                    SubModalidad = da.TN_IdSubModalidad.HasValue ? new SubModalidad
                    {
                        IdSubModalidad = da.TN_IdSubModalidad.Value,
                        Nombre = da.TC_NombreSubModalidad
                    } : null
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
                var urgenteParam = new SqlParameter("@PB_Urgente", solicitudProveedor.Urgente);
                var aprobadoParam = new SqlParameter("@PB_Aprobado", solicitudProveedor.Aprobado);
                var fechaCreacionParam = new SqlParameter("@PF_FechaCreacion", solicitudProveedor.FechaCrecion);
                var idUsuarioCreadorParam = new SqlParameter("@PN_IdUsuarioCreador", solicitudProveedor.UsuarioCreador.IdUsuario);
                var idDelitoParam = new SqlParameter("@PN_IdDelito", solicitudProveedor.Delito.IdDelito);
                var idCategoriaDelitoParam = new SqlParameter("@PN_IdCategoriaDelito", solicitudProveedor.CategoriaDelito.IdCategoriaDelito);
                var idModalidadParam = new SqlParameter("@PN_IdModalidad", solicitudProveedor.Modalidad.IdModalidad == 0 ? (object)DBNull.Value : solicitudProveedor.Modalidad.IdModalidad);
                var idSubModalidadParam = new SqlParameter("@PN_IdSubModalidad", solicitudProveedor.SubModalidad.IdSubModalidad == 0 ? (object)DBNull.Value : solicitudProveedor.Modalidad.IdModalidad);
                var idEstadoParam = new SqlParameter("@PN_IdEstado", solicitudProveedor.Estado.IdEstado);
                var idProveedorParam = new SqlParameter("@PN_IdProveedor", solicitudProveedor.Proveedor.IdProveedor);
                var idFiscaliaParam = new SqlParameter("@PN_IdFiscalia", solicitudProveedor.Fiscalia.IdFiscalia);
                var idOficinaParam = new SqlParameter("@PN_IdOficina", solicitudProveedor.Oficina.IdOficina);

                var idSolicitudParam = new SqlParameter("@IdSolicitudInsertada", System.Data.SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output // Parámetro de salida
                };

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_InsertarSolicitudProveedor @PN_NumeroUnico, @PN_NumeroCaso, @PC_Imputado, @PC_Ofendido, @PC_Resennia," +
                    " @PB_Urgente, @PB_Aprobado, @PF_FechaCreacion, @PN_IdUsuarioCreador," +
                    " @PN_IdDelito, @PN_IdCategoriaDelito, @PN_IdModalidad, @PN_IdSubModalidad, @PN_IdEstado, @PN_IdProveedor, @PN_IdFiscalia," +
                    " @PN_IdOficina, @IdSolicitudInsertada OUTPUT",
                    numeroUnicoParam, numeroCasoParam, imputadoParam, ofendidoParam, resenniaParam, urgenteParam,
                    aprobadoParam, fechaCreacionParam, idUsuarioCreadorParam, idDelitoParam, idCategoriaDelitoParam,
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

        public async Task<List<string>> ListarNumerosUnicosTramitados()
        {
            try
            {
                // Ejecutar el procedimiento almacenado para consultar
                var numerosUnicosTramitados = await _context.Database
                        .SqlQuery<string>($"EXEC PA_ListarNumerosUnicosTramitados")
                        .ToListAsync();
                Console.WriteLine(numerosUnicosTramitados);

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

        public async Task<bool> MoverEstadoASinEfecto(int idSolicitudProveedor, int idUsuario, string? observacion)
        {
            try
            {
                //Definir los parámetros para el procedimiento almacenado
                var idSolicitudProveedorParam = new SqlParameter("@pTN_IdSolicitud", idSolicitudProveedor);
                var idUsuarioParam = new SqlParameter("@PN_IdUsuario", idUsuario);
                var observacionParam = new SqlParameter("@PC_Observacion", observacion)
                {
                    Size = 255,
                    Value = (object)observacion ?? DBNull.Value // Manejar nulos
                };

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_ActualizarEstadoSinEfectoSolicitudProveedor @pTN_IdSolicitud, @PN_IdUsuario, @PC_Observacion",
                    idSolicitudProveedorParam, idUsuarioParam, observacionParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al insertar al cambiar el estado de solicitud de proveedor.");
                }


                return resultado >= 0 ? true : false;

            }
            catch (SqlException ex)
            {
                // Si el error proviene de SQL Server, se captura el mensaje del procedimiento almacenado
                throw new Exception($"Error en la base de datos al cambiar el estado de solicitud de proveedor: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al cambiar el estado de solicitud de proveedor: {ex.Message}", ex);
            }
        }

        public async Task<SolicitudProveedor> obtenerSolicitud(int idSolicitud)
        {
            try
            {
                var idSolicitudParam = new SqlParameter("@pTN_IdSolicitud", idSolicitud);

                // Ejecutar el procedimiento almacenado
                var resultadoConsulta = await _context.TSOLITEL_SolicitudProveedorDA
                    .FromSqlRaw("EXEC dbo.PA_ConsultarSolicitudesProveedor @pTN_IdSolicitud", idSolicitudParam)
                    .ToListAsync();

                var da = resultadoConsulta.FirstOrDefault();

                if (da == null)
                {
                    return null;  // Manejar el caso donde no hay resultados
                }

                // Mapeo de los resultados
                var solicitudProveedor =  new SolicitudProveedor
                {
                    IdSolicitudProveedor = da.TN_IdSolicitud,
                    NumeroUnico = da.TC_NumeroUnico,
                    NumeroCaso = da.TC_NumeroCaso,
                    Imputado = da.TC_Imputado,
                    Ofendido = da.TC_Ofendido,
                    Resennia = da.TC_Resennia,
                    Urgente = da.TB_Urgente,
                    Aprobado = da.TB_Aprobado,
                    FechaCrecion = da.TF_FechaDeCreacion,
                    Proveedor = new Proveedor { IdProveedor = da.TN_IdProveedor, Nombre = da.TC_NombreProveedor },
                    Delito = new Delito { IdDelito = da.TN_IdDelito, IdCategoriaDelito = da.TN_IdCategoriaDelito, Nombre = da.TC_NombreDelito },
                    CategoriaDelito = new CategoriaDelito { Nombre = da.TC_NombreCategoriaDelito, IdCategoriaDelito = da.TN_IdCategoriaDelito },
                    Estado = new Estado { IdEstado = da.TN_IdEstado, Nombre = da.TC_NombreEstado },
                    Fiscalia = new Fiscalia { IdFiscalia = da.TN_IdFiscalia, Nombre = da.TC_NombreFiscalia },
                    Modalidad = new Modalidad { IdModalidad = (int)(da.TN_IdModalidad == null ? 0 : da.TN_IdModalidad), Nombre = da.TC_NombreModalidad },
                    SubModalidad = new SubModalidad { IdSubModalidad = (int)(da.TN_IdSubModalidad == null ? 0 : da.TN_IdSubModalidad), Nombre = da.TC_NombreSubModalidad, IdModalidad = (int)(da.TN_IdModalidad == null ? 0 : da.TN_IdModalidad) },
                    UsuarioCreador = new Usuario { IdUsuario = da.TN_IdUsuario, Nombre = da.TC_NombreUsuario },
                    Oficina = new Oficina { IdOficina = da.TN_IdOficina,Nombre = da.TC_NombreOficina }


                };

                return solicitudProveedor;
            }
            catch (SqlException ex)
            {
                // Captura el error específico de SQL Server
                throw new Exception($"Error en la base de datos al obtener solicitudProveedor: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al obtener solicitudProveedor: {ex.Message}", ex);
            }
        }

        public async Task<List<SolicitudProveedor>> obtenerSolicitudesProveedor(int idEstado, DateTime? fechainicio, DateTime? fechaFin, string? numeroUnico, int? idOficina, int? idUsuario, int? idSolicitud)
        {

            try
            {
                var idEstadoParam = new SqlParameter("@pTN_IdEstado", (object)idEstado ?? DBNull.Value);
                var fechainicioParam = new SqlParameter("@pTF_FechaInicio", (object)fechainicio ?? DBNull.Value);
                var fechaFinParam = new SqlParameter("@pTF_FechaFin", (object)fechaFin ?? DBNull.Value);
                var numeroUnicoParam = new SqlParameter("@pTC_NumeroUnico", (object)numeroUnico ?? DBNull.Value);
                var idOficinaParam = new SqlParameter("@pTN_IdOficina", (object)idOficina ?? DBNull.Value);
                var idUsuarioParam = new SqlParameter("@pTN_IdUsuario", (object)idUsuario ?? DBNull.Value);
                var idSolicitudParam = new SqlParameter("@pTN_IdSolicitud", (object)idSolicitud ?? DBNull.Value);

                // Ejecutar el procedimiento almacenado
                var solicitudesProveedorDA = await _context.TSOLITEL_SolicitudProveedorDA
                    .FromSqlRaw("EXEC dbo.PA_ConsultarSolicitudesProveedor @pTN_IdSolicitud, @pTN_IdEstado, @pTF_FechaInicio, @pTF_FechaFin, @pTC_NumeroUnico, @pTN_IdOficina, " +
                    "@pTN_IdUsuario", idSolicitudParam, idEstadoParam, fechainicioParam, fechaFinParam, numeroUnicoParam, idOficinaParam, idUsuarioParam )
                    .ToListAsync();

                // Mapeo de los resultados
                var solicitudesProveedor = solicitudesProveedorDA.Select(da => new SolicitudProveedor
                {
                    IdSolicitudProveedor = da.TN_IdSolicitud,
                    NumeroUnico = da.TC_NumeroUnico,
                    NumeroCaso = da.TC_NumeroCaso,
                    Imputado = da.TC_Imputado,
                    Ofendido = da.TC_Ofendido,
                    Resennia = da.TC_Resennia,
                    Urgente = da.TB_Urgente,
                    Aprobado = da.TB_Aprobado,
                    FechaCrecion = da.TF_FechaDeCreacion,
                    Proveedor = new Proveedor { IdProveedor = da.TN_IdProveedor, Nombre = da.TC_NombreProveedor },
                    Delito = new Delito { IdDelito = da.TN_IdDelito, IdCategoriaDelito = da.TN_IdCategoriaDelito, Nombre = da.TC_NombreDelito },
                    CategoriaDelito = new CategoriaDelito { Nombre = da.TC_NombreCategoriaDelito , IdCategoriaDelito = da.TN_IdCategoriaDelito},
                    Estado = new Estado { IdEstado = da.TN_IdEstado, Nombre = da.TC_NombreEstado },
                    Fiscalia = new Fiscalia { IdFiscalia = da.TN_IdFiscalia, Nombre = da.TC_NombreFiscalia},
                    Modalidad = new Modalidad { IdModalidad = (int)(da.TN_IdModalidad == null ? 0: da.TN_IdModalidad), Nombre = da.TC_NombreModalidad },
                    SubModalidad = new SubModalidad { IdSubModalidad = (int)(da.TN_IdSubModalidad == null ? 0: da.TN_IdSubModalidad), Nombre = da.TC_NombreSubModalidad, IdModalidad = (int)(da.TN_IdModalidad == null ? 0 : da.TN_IdModalidad) },
                    UsuarioCreador = new Usuario { IdUsuario = da.TN_IdUsuario, Nombre = da.TC_NombreUsuario, Apellido = da.TC_ApellidoUsuario },
                    Oficina = new Oficina { IdOficina = da.TN_IdOficina, Nombre = da.TC_NombreOficina }
                    

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
                throw new Exception($"1111Ocurrió un error inesperado al obtener la lista de solicitudesProveedor: {ex.Message}", ex);
            }
        }

        public async Task<List<SolicitudProveedor>> obtenerSolicitudesProveedorPorEstado(int pageNumber, int pageSize, int idEstado)
        {
            try
            {
                var pageNumberParam = new SqlParameter("@PageNumber", pageNumber);
                var pageSizeParam = new SqlParameter("@PageSize", pageSize);
                var idEstadoParam = new SqlParameter("@IdEstado", idEstado);

                // Ejecutar el procedimiento almacenado
                var solicitudesProveedorDA = await _context.TSOLITEL_SolicitudProveedorDA
                    .FromSqlRaw("EXEC PA_ConsultarSolicitudesProveedorPorEstado @PageNumber, @PageSize, @IdEstado", pageNumberParam, pageSizeParam, idEstadoParam)
                    .ToListAsync();

                // Mapeo de los resultados
                var solicitudesProveedor = solicitudesProveedorDA.Select(da => new SolicitudProveedor
                {
                    IdSolicitudProveedor = da.TN_IdSolicitud,
                    NumeroUnico = da.TC_NumeroUnico,
                    NumeroCaso = da.TC_NumeroCaso != null ? da.TC_NumeroCaso: null,
                    Imputado = da.TC_Imputado,
                    Ofendido = da.TC_Ofendido,
                    Resennia = da.TC_Resennia,
                    Urgente = da.TB_Urgente,
                    Aprobado = da.TB_Aprobado,
                    FechaCrecion = da.TF_FechaDeCreacion,
                    Proveedor = new Proveedor { IdProveedor = da.TN_IdProveedor, Nombre = da.TC_NombreProveedor },
                    Delito = new Delito { IdDelito = da.TN_IdDelito, IdCategoriaDelito = da.TN_IdCategoriaDelito, Nombre = da.TC_NombreDelito },
                    CategoriaDelito = new CategoriaDelito { Nombre = da.TC_NombreCategoriaDelito, IdCategoriaDelito = da.TN_IdCategoriaDelito },
                    Estado = new Estado { IdEstado = da.TN_IdEstado, Nombre = da.TC_NombreEstado },
                    Fiscalia = new Fiscalia { IdFiscalia = da.TN_IdFiscalia, Nombre = da.TC_NombreFiscalia },
                    Modalidad = da.TN_IdModalidad != null ? new Modalidad { IdModalidad = (int)da.TN_IdModalidad, Nombre = da.TC_NombreModalidad }: null,
                    SubModalidad = da.TN_IdSubModalidad != null ? new SubModalidad { IdSubModalidad = (int)da.TN_IdSubModalidad, Nombre = da.TC_NombreSubModalidad, IdModalidad = (int)da.TN_IdModalidad }:null,
                    UsuarioCreador = new Usuario { IdUsuario = da.TN_IdUsuario, Nombre = da.TC_NombreUsuario}

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

        public async Task<bool> relacionarRequerimientos(List<int> idSolicitudes, List<int> idRequerimientos)
        {
            try
            {
                for (int i = 0; i < idSolicitudes.Count; i++)
                {
                    for (int j = 0; j < idRequerimientos.Count; j++)
                    {
                        //Definir los parámetros para el procedimiento almacenado
                        var idSolicitudParam = new SqlParameter("@PN_IdSolicitudProveedor", idSolicitudes[i]);
                        var idRequerimientoParam = new SqlParameter("@PN_IdRequerimientoProveedor", idRequerimientos[j]);

                        // Ejecutar el procedimiento almacenado para insertar
                        await _context.Database.ExecuteSqlRawAsync(
                            "EXEC PA_RelacionarRequerimientosProveedor @PN_IdSolicitudProveedor, @PN_IdRequerimientoProveedor",
                            idSolicitudParam, idRequerimientoParam);
                    }
                }

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al insertar las relaciones de requerimientos.");
                }

                return resultado >= 0 ? true : false;


            }
            catch (SqlException ex)
            {
                // Si el error proviene de SQL Server, se captura el mensaje del procedimiento almacenado
                throw new Exception($"Error en la base de datos al insertar las relaciones de requerimientos: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al insertar las relaciones de requerimientos: {ex.Message}", ex);
            }
        }

        public async Task<bool> DevolverATramitado(int id, int idUsuario, string observacion = null)
        {
            try
            {
                //Definir los parámetros para el procedimiento almacenado
                var idSolicitudProveedorParam = new SqlParameter("@pTN_IdSolicitud", id);
                var idUsuarioParam = new SqlParameter("@pTN_IdUsuario", idUsuario);
                var observacionParam = new SqlParameter("@pTC_Observacion", observacion)
                {
                    Size = 255,
                    Value = (object)observacion ?? DBNull.Value // Manejar nulos
                };


                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_DevolverATramitado @pTN_IdSolicitud, @pTN_IdUsuario, @pTC_Observacion",
                    idSolicitudProveedorParam, idUsuarioParam, observacionParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al insertar al aprobar la solicitud.");
                }


                return resultado >= 0 ? true : false;

            }
            catch (SqlException ex)
            {
                // Si el error proviene de SQL Server, se captura el mensaje del procedimiento almacenado
                throw new Exception($"Error en la base de datos al aprobar la solicitud: {ex.Message}", ex);
            }
        }

        public async Task<bool> ActualizarEstadoTramitado(int idSolicitudProveedor, int idUsuario, string? observacion)
        {
            try
            {
                //Definir los parámetros para el procedimiento almacenado
                var idSolicitudProveedorParam = new SqlParameter("@pIdSolicitudProveedor", idSolicitudProveedor);
                var idUsuarioParam = new SqlParameter("@pIdUsuario", idUsuario);
                var observacionParam = new SqlParameter("@pObservacion", observacion)
                {
                    Size = 255,
                    Value = (object)observacion ?? DBNull.Value // Manejar nulos
                };

                // Ejecutar el procedimiento almacenado para insertar
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC PA_ActualizarTramitadoSolicitudProveedor @pIdSolicitudProveedor, @pObservacion, @pIdUsuario",
                    idSolicitudProveedorParam, observacionParam, idUsuarioParam);

                var resultado = await _context.SaveChangesAsync();

                if (resultado < 0)
                {
                    throw new Exception("Error al insertar al cambiar el estado de solicitud de proveedor.");
                }


                return resultado >= 0 ? true : false;

            }
            catch (SqlException ex)
            {
                // Si el error proviene de SQL Server, se captura el mensaje del procedimiento almacenado
                throw new Exception($"Error en la base de datos al cambiar el estado de solicitud de proveedor: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al cambiar el estado de solicitud de proveedor: {ex.Message}", ex);
            }
        }

        public async Task<SolicitudProveedor> ConsultarSolicitudProveedorPorNumeroUnico(string numeroUnico)
        {
            try
            {
                var numeroUnicoParam = new SqlParameter("@PN_NumeroUnico", numeroUnico);

                // Ejecutar el procedimiento almacenado
                var solicitudesProveedorDA = await _context.TSOLITEL_SolicitudProveedorDA
                    .FromSqlRaw("EXEC PA_ConsultarSolicitudProveedorPorNumeroUnico @PN_NumeroUnico", numeroUnicoParam)
                    .ToListAsync();

                var solicitudProveedor = solicitudesProveedorDA.FirstOrDefault();

                if(solicitudProveedor == null)
                {
                    return null;
                }

                

                var solicitudProveedorRespuesta = new SolicitudProveedor
                {
                    CategoriaDelito = new CategoriaDelito { IdCategoriaDelito = solicitudProveedor.TN_IdCategoriaDelito, Nombre = solicitudProveedor.TC_NombreCategoriaDelito},
                    Delito = new Delito { IdDelito = solicitudProveedor.TN_IdDelito, Nombre = solicitudProveedor.TC_NombreDelito },
                    Fiscalia = new Fiscalia { IdFiscalia = solicitudProveedor.TN_IdFiscalia, Nombre = solicitudProveedor.TC_NombreFiscalia },
                    Imputado = solicitudProveedor.TC_Imputado,
                    Ofendido = solicitudProveedor.TC_Ofendido,
                    Resennia = solicitudProveedor.TC_Resennia
                    
                };


                return solicitudProveedorRespuesta;

            }
            catch (SqlException ex)
            {
                // Si el error proviene de SQL Server, se captura el mensaje del procedimiento almacenado
                throw new Exception($"Error en la base de datos al cambiar el estado de solicitud de proveedor: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de excepción
                throw new Exception($"Ocurrió un error inesperado al cambiar el estado de solicitud de proveedor: {ex.Message}", ex);
            }
        }

        public async Task<List<SolicitudProveedor>> ObtenerSolicitudesProveedorPorId(int idSolicitud) // SISTEMA PROVEEDOR
        {
            try
            {
                var idSolicitudParam = new SqlParameter("@pTN_IdSolicitud", idSolicitud == 0 ? (object)DBNull.Value : idSolicitud);

                var solicitudesProveedorDA = await _context.TSOLITEL_SolicitudProveedorDA
                    .FromSqlRaw("EXEC PA_ConsultarSolicitudesProveedor @pTN_IdSolicitud", idSolicitudParam)
                    .ToListAsync();

                var solicitudesProveedor = solicitudesProveedorDA.Select(da => new SolicitudProveedor
                {
                    IdSolicitudProveedor = da.TN_IdSolicitud,
                    NumeroUnico = da.TC_NumeroUnico,
                    NumeroCaso = da.TC_NumeroCaso,
                    Imputado = da.TC_Imputado,
                    Ofendido = da.TC_Ofendido,
                    Resennia = da.TC_Resennia,
                    Urgente = da.TB_Urgente,
                    Aprobado = da.TB_Aprobado,
                    FechaCrecion = da.TF_FechaDeCreacion,
                    UsuarioCreador = new Usuario
                    {
                        IdUsuario = da.TN_IdUsuario,
                        Nombre = da.TC_NombreUsuario
                    },
                    Proveedor = new Proveedor
                    {
                        IdProveedor = da.TN_IdProveedor,
                        Nombre = da.TC_NombreProveedor
                    },
                    Delito = new Delito
                    {
                        IdDelito = da.TN_IdDelito,
                        Nombre = da.TC_NombreDelito
                    },
                    CategoriaDelito = new CategoriaDelito
                    {
                        IdCategoriaDelito = da.TN_IdCategoriaDelito,
                        Nombre = da.TC_NombreCategoriaDelito
                    },
                    Estado = new Estado
                    {
                        IdEstado = da.TN_IdEstado,
                        Nombre = da.TC_NombreEstado
                    },
                    Fiscalia = new Fiscalia
                    {
                        IdFiscalia = da.TN_IdFiscalia,
                        Nombre = da.TC_NombreFiscalia
                    },
                    Modalidad = da.TN_IdModalidad.HasValue ? new Modalidad
                    {
                        IdModalidad = da.TN_IdModalidad.Value,
                        Nombre = da.TC_NombreModalidad
                    } : null,
                    SubModalidad = da.TN_IdSubModalidad.HasValue ? new SubModalidad
                    {
                        IdSubModalidad = da.TN_IdSubModalidad.Value,
                        Nombre = da.TC_NombreSubModalidad
                    } : null
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
