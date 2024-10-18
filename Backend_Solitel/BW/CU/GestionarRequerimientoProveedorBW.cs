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

        public async Task<bool> InsertarRequerimientoProveedor(RequerimientoProveedor requerimientoProveedor)
        {
            //Reglas de Negocio
            return await this.gestionarRequerimientoProveedorDA.InsertarRequerimientoProveedor(requerimientoProveedor);
        }
    }
}
