using BC.Modelos;
using BW.Interfaces.BW;
using BW.Interfaces.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.CU
{
    public class GestionarSolicitudProveedorBW : IGestionarSolicitudProveedorBW
    {
        private readonly IGestionarSolicitudProveedorDA gestionarSolicitudProveedorDA;

        public GestionarSolicitudProveedorBW(IGestionarSolicitudProveedorDA dA)
        {
            gestionarSolicitudProveedorDA = dA;
        }

        public async Task<bool> AprobarSolicitudProveedor(int idSolicitudProveedor, int idUsuario, string? observacion)
        {
            return await this.gestionarSolicitudProveedorDA.AprobarSolicitudProveedor(idSolicitudProveedor, idUsuario, observacion);
        }

        public async Task<bool> ActualizarEstadoFinalizado(int id, int idUsuario, string observacion = null)
        {
            return await this.gestionarSolicitudProveedorDA.ActualizarEstadoFinalizado(id, idUsuario, observacion);
        }

        public async Task<bool> ActualizarEstadoLegajo(int id, int idUsuario, string observacion = null)
        {
            return await this.gestionarSolicitudProveedorDA.ActualizarEstadoLegajo(id, idUsuario, observacion);
        }

        public async Task<List<SolicitudProveedor>> consultarSolicitudesProveedorPorNumeroUnico(string numeroUnico)
        {
            return await this.gestionarSolicitudProveedorDA.consultarSolicitudesProveedorPorNumeroUnico(numeroUnico);
        }

        public Task<int> InsertarSolicitudProveedor(SolicitudProveedor solicitudProveedor)
        {
            //Aplicar reglas de negocio
            return this.gestionarSolicitudProveedorDA.InsertarSolicitudProveedor(solicitudProveedor);
        }

        public async Task<List<string>> ListarNumerosUnicosTramitados()
        {
            return await this.gestionarSolicitudProveedorDA.ListarNumerosUnicosTramitados();
        }

        public async Task<bool> MoverEstadoASinEfecto(int idSolicitudProveedor, int idUsuario, string? observacion)
        {
            return await this.gestionarSolicitudProveedorDA.MoverEstadoASinEfecto(idSolicitudProveedor, idUsuario, observacion);
        }

        public async Task<SolicitudProveedor> obtenerSolicitud(int idSolicitud)
        {
            return await this.gestionarSolicitudProveedorDA.obtenerSolicitud(idSolicitud);
        }

        public Task<List<SolicitudProveedor>> obtenerSolicitudesProveedor(int idEstado, DateTime? fechainicio, DateTime? fechaFin, string? numeroUnico, int? idOficina, int? idUsuario, int? idSolicitud)
        {
            //Aplicar reglas de negocio
            return this.gestionarSolicitudProveedorDA.obtenerSolicitudesProveedor(idEstado, fechainicio, fechaFin, numeroUnico, idOficina, idUsuario, idSolicitud);
        }

        public Task<List<SolicitudProveedor>> obtenerSolicitudesProveedorPorEstado(int pageNumber, int pageSize, int idEstado)
        {
            return this.gestionarSolicitudProveedorDA.obtenerSolicitudesProveedorPorEstado(pageNumber, pageSize, idEstado);
        }

        public async Task<bool> relacionarRequerimientos(List<int> idSolicitudes, List<int> idRequerimientos)
        {
            return await this.gestionarSolicitudProveedorDA.relacionarRequerimientos(idSolicitudes, idRequerimientos);
        }

        public async Task<bool> DevolverATramitado(int id, int idUsuario, string observacion = null)
        {
            return await this.gestionarSolicitudProveedorDA.DevolverATramitado(id, idUsuario, observacion);
        }

        public async Task<bool> ActualizarEstadoTramitado(int idSolicitudProveedor, int idUsuario, string? observacion)
        {
            return await this.gestionarSolicitudProveedorDA.ActualizarEstadoTramitado(idSolicitudProveedor, idUsuario, observacion);
        }

        public async Task<SolicitudProveedor> ConsultarSolicitudProveedorPorNumeroUnico(string numeroUnico)
        {
            return await this.gestionarSolicitudProveedorDA.ConsultarSolicitudProveedorPorNumeroUnico(numeroUnico);
        }

        public async Task<List<SolicitudProveedor>> ObtenerSolicitudesProveedorPorId(int idSolicitud) // SISTEMA PROVEEDOR
        {
            return await this.gestionarSolicitudProveedorDA.ObtenerSolicitudesProveedorPorId(idSolicitud);
        }
    }
}

