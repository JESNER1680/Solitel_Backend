using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.DA
{
    public interface IGestionarHistorialDA
    {
        public Task<List<Historial>> ConsultarHistorialDeSolicitudProveedor(int idSolicitudProveedor);

        public Task<List<Historial>> ConsultarHistorialDeSolicitudAnalisis(int idSolicitudProveedor);
    }
}
