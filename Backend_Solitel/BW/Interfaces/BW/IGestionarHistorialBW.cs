using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.BW
{
    public interface IGestionarHistorialBW
    {
        public Task<List<Historial>> ConsultarHistorialDeSolicitudProveedor(int idSolicitudProveedor);

        public Task<List<Historial>> ConsultarHistorialDeSolicitudAnalisis(int idSolicitudProveedor);
    }
}
