using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.BW
{
    public interface IGestionarTipoSolicitudBW
    {
        public Task<TipoSolicitud> insertarTipoSolicitud(TipoSolicitud tipoSolicitud);

        public Task<List<TipoSolicitud>> obtenerTipoSolicitud();

        public Task<TipoSolicitud> eliminarTipoSolicitud(int id);
    }
}
