using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.BW
{
    public interface IGestionarProveedorBW
    {
        public Task<Proveedor> InsertarProveedor(Proveedor proveedor);

        public Task<List<Proveedor>> ConsultarProveedores();

        public Task<bool> EliminarProveedor(int idProveedor);

        public Task<Proveedor> ConsultarProveedor(int idProveedor);
    }
}
