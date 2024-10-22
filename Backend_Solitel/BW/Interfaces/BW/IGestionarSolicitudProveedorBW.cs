using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.BW
{
    public interface IGestionarSolicitudProveedorBW
    {
        public Task<int> InsertarSolicitudProveedor(SolicitudProveedor solicitudProveedor);

        public Task<List<SolicitudProveedor>> obtenerSolicitudesProveedor(int pageNumber, int pageSize);
    }
}
