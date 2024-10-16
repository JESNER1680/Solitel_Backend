using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.DA
{
    public interface IGestionarSolicitudAnalistaDA
    {
        public Task<bool>CrearSolicitudAnalista(SolicitudAnalisis solicitudAnalisis);
    }
}
