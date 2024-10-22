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

        public Task<int> InsertarSolicitudProveedor(SolicitudProveedor solicitudProveedor)
        {
            //Aplicar reglas de negocio
            return this.gestionarSolicitudProveedorDA.InsertarSolicitudProveedor(solicitudProveedor);
        }

        public Task<List<SolicitudProveedor>> obtenerSolicitudesProveedor(int pageNumber, int pageSize)
        {
            //Aplicar reglas de negocio
            return this.gestionarSolicitudProveedorDA.obtenerSolicitudesProveedor(pageNumber, pageSize);
        }
    }
}
