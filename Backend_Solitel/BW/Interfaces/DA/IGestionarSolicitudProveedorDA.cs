using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.DA
{
    public interface IGestionarSolicitudProveedorDA
    {
        public Task<List<SolicitudProveedor>> obtenerSolicitudesProveedor(int idEstado, DateTime? fechainicio, DateTime? fechaFin, string? numeroUnico, int? idOficina, int? idUsuario, int? idSolicitud);

        public Task<int> InsertarSolicitudProveedor(SolicitudProveedor solicitudProveedor);

        public Task<List<string>> ListarNumerosUnicosTramitados();

        public Task<List<SolicitudProveedor>> consultarSolicitudesProveedorPorNumeroUnico(string numeroUnico);

        public Task<bool> relacionarRequerimientos(List<int> idSolicitudes, List<int> idRequerimientos);

        public Task<bool> MoverEstadoASinEfecto(int idSolicitudProveedor, int idUsuario, string? observacion);

        public Task<List<SolicitudProveedor>> obtenerSolicitudesProveedorPorEstado(int pageNumber, int pageSize, int idEstado);

        public Task<bool> AprobarSolicitudProveedor(int idSolicitudProveedor, int idUsuario, string? observacion);
      
        public Task<bool> ActualizarEstadoLegajo(int id, int idUsuario, string observacion = null);

        public Task<bool> ActualizarEstadoFinalizado(int id, int idUsuario, string observacion = null);

        public Task<bool> DevolverATramitado(int id, int idUsuario, string observacion = null);

        public Task<bool> ActualizarEstadoTramitado(int idSolicitudProveedor, int idUsuario, string? observacion);

        public Task<SolicitudProveedor> ConsultarSolicitudProveedorPorNumeroUnico(string numeroUnico);

        public Task<SolicitudProveedor> obtenerSolicitud(int idSolicitud);

    }
}
