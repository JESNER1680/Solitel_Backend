using BC.Modelos;
using BC.Reglas_de_Negocio;
using BW.Interfaces.BW;
using BW.Interfaces.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.CU
{
    public class GestionarProveedorBW : IGestionarProveedorBW
    {
        private readonly IGestionarProveedorDA gestionarProveedorDA;

        public GestionarProveedorBW(IGestionarProveedorDA gestionarProveedorDA)
        {
            this.gestionarProveedorDA = gestionarProveedorDA;
        }

        public async Task<Proveedor> ConsultarProveedor(int idProveedor)
        {
            return await this.gestionarProveedorDA.ConsultarProveedor(idProveedor);
        }

        public Task<List<Proveedor>> ConsultarProveedores()
        {
            return this.gestionarProveedorDA.ConsultarProveedores();
        }

        public Task<bool> EliminarProveedor(int idProveedor)
        {
            return this.gestionarProveedorDA.EliminarProveedor(idProveedor);
        }

        public Task<Proveedor> InsertarProveedor(Proveedor proveedor)
        {
            ProveedorBR.ValidarProveedor(proveedor);
            return this.gestionarProveedorDA.InsertarProveedor(proveedor);
        }
    }
}
