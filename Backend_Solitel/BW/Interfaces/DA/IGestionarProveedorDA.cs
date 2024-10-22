using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.DA
{
    public interface IGestionarProveedorDA
    {
        public Task<bool> InsertarProveedor(Proveedor proveedor);

        public Task<List<Proveedor>> ConsultarProveedores();

        public Task<bool> EliminarProveedor(int idProveedor);
    }
}
