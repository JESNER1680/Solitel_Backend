using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.BW
{
    public interface IGestionarRequerimientoProveedorBW
    {
        public Task<int> InsertarRequerimientoProveedor(RequerimientoProveedor requerimientoProveedor);

        public Task<List<RequerimientoProveedor>> ConsultarRequerimientosProveedor(int idSolicitudProveedor);

        public Task<List<DatoRequerido>> ConsultarDatosRequeridos(int idRequerimientoProveedor);

        public Task<List<TipoSolicitud>> ConsultarTipoSolicitudes(int idRequerimientoProveedor);
    }
}
