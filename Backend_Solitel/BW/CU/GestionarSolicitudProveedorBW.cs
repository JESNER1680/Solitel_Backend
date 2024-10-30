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

        public async Task<bool> MoverEstadoASinEfecto(int idSolicitudProveedor)
        {
            return await this.gestionarSolicitudProveedorDA.MoverEstadoASinEfecto(idSolicitudProveedor);
        }

        public async Task<SolicitudProveedor> obtenerSolicitud(int idSolicitud)
        {
            return await this.gestionarSolicitudProveedorDA.obtenerSolicitud(idSolicitud);
        }

        public Task<List<SolicitudProveedor>> obtenerSolicitudesProveedor(int pageNumber, int pageSize)
        {
            //Aplicar reglas de negocio
            return this.gestionarSolicitudProveedorDA.obtenerSolicitudesProveedor(pageNumber, pageSize);
        }

        public Task<List<SolicitudProveedor>> obtenerSolicitudesProveedorPorEstado(int pageNumber, int pageSize, int idEstado)
        {
            return this.gestionarSolicitudProveedorDA.obtenerSolicitudesProveedorPorEstado(pageNumber, pageSize, idEstado);
        }

        public async Task<bool> relacionarRequerimientos(List<int> idSolicitudes, List<int> idRequerimientos)
        {
            return await this.gestionarSolicitudProveedorDA.relacionarRequerimientos(idSolicitudes, idRequerimientos);
        }
    }
}
