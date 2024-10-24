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

        public async Task<List<SolicitudProveedor>> consultarSolicitudesProveedorPorNumeroUnico(int numeroUnico)
        {
            return await this.gestionarSolicitudProveedorDA.consultarSolicitudesProveedorPorNumeroUnico(numeroUnico);
        }

        public Task<int> InsertarSolicitudProveedor(SolicitudProveedor solicitudProveedor)
        {
            //Aplicar reglas de negocio
            return this.gestionarSolicitudProveedorDA.InsertarSolicitudProveedor(solicitudProveedor);
        }

        public async Task<List<int>> ListarNumerosUnicosTramitados()
        {
            return await this.gestionarSolicitudProveedorDA.ListarNumerosUnicosTramitados();
        }

        public Task<List<SolicitudProveedor>> obtenerSolicitudesProveedor(int pageNumber, int pageSize)
        {
            //Aplicar reglas de negocio
            return this.gestionarSolicitudProveedorDA.obtenerSolicitudesProveedor(pageNumber, pageSize);
        }
    }
}
