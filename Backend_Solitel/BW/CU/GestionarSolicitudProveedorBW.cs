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
        private readonly IGestionarSolicitudProveedorDA _DA;

        public GestionarSolicitudProveedorBW(IGestionarSolicitudProveedorDA dA)
        {
            _DA = dA;
        }

        public Task<int> InsertarSolicitudProveedor(SolicitudProveedor solicitudProveedor)
        {
            //Aplicar reglas de negocio
            return this._DA.InsertarSolicitudProveedor(solicitudProveedor);
        }
    }
}
