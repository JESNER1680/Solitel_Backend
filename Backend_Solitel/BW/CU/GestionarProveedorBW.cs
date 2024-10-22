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
    public class GestionarProveedorBW : IGestionarProveedorBW
    {
        private readonly IGestionarProveedorDA gestionarProveedorDA;

        public GestionarProveedorBW(IGestionarProveedorDA gestionarProveedorDA)
        {
            this.gestionarProveedorDA = gestionarProveedorDA;
        }

        public Task<List<Proveedor>> ConsultarProveedores()
        {
            //Reglas de Negocio
            return this.gestionarProveedorDA.ConsultarProveedores();
        }

        public Task<bool> EliminarProveedor(int idProveedor)
        {
            //Reglas de Negocio
            return this.gestionarProveedorDA.EliminarProveedor(idProveedor);
        }

        public Task<bool> InsertarProveedor(Proveedor proveedor)
        {
            //Reglas de Negocio
            return this.gestionarProveedorDA.InsertarProveedor(proveedor);
        }
    }
}
