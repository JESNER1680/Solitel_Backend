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
    public class GestionarRequerimientoProveedorBW : IGestionarRequerimientoProveedorBW
    {
        private readonly IGestionarRequerimientoProveedorDA gestionarRequerimientoProveedorDA;

        public GestionarRequerimientoProveedorBW(IGestionarRequerimientoProveedorDA gestionarRequerimientoProveedorDA)
        {
            this.gestionarRequerimientoProveedorDA = gestionarRequerimientoProveedorDA;
        }

        public async Task<List<DatoRequerido>> ConsultarDatosRequeridos(int idRequerimientoProveedor)
        {
            return await this.gestionarRequerimientoProveedorDA.ConsultarDatosRequeridos(idRequerimientoProveedor);
        }

        public async Task<List<RequerimientoProveedor>> ConsultarRequerimientosProveedor(int idSolicitudProveedor)
        {
            return await this.gestionarRequerimientoProveedorDA.ConsultarRequerimientosProveedor(idSolicitudProveedor);
        }

        public Task<List<TipoSolicitud>> ConsultarTipoSolicitudes(int idRequerimientoProveedor)
        {
            return this.gestionarRequerimientoProveedorDA.ConsultarTipoSolicitudes(idRequerimientoProveedor);
        }

        public async Task<int> InsertarRequerimientoProveedor(RequerimientoProveedor requerimientoProveedor)
        {
            //Reglas de Negocio
            return await this.gestionarRequerimientoProveedorDA.InsertarRequerimientoProveedor(requerimientoProveedor);
        }
    }
}
