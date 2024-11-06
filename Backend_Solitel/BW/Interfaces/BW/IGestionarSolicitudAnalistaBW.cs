using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.BW
{
    public interface IGestionarSolicitudAnalistaBW
    {
        public Task<bool> CrearSolicitudAnalista(SolicitudAnalisis solicitudAnalisis);

        public Task<List<SolicitudAnalisis>> ConsultarSolicitudesAnalisisAsync(int pageNumber, int pageSize,
            int? idEstado = null, string numeroUnico = null, DateTime? fechaInicio = null,
            DateTime? fechaFin = null, string caracterIngresado = null);
        public Task<List<SolicitudAnalisis>> ObtenerSolicitudesAnalisis();
    }
}
