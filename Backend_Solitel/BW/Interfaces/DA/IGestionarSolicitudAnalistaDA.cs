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

        public Task<List<SolicitudAnalisis>> ConsultarSolicitudesAnalisisAsync(int pageNumber, int pageSize, 
            int? idEstado = null, string numeroUnico = null, DateTime? fechaInicio = null, 
            DateTime? fechaFin = null, string caracterIngresado = null);
        public Task<List<SolicitudAnalisis>> ObtenerSolicitudesAnalisis();

        public Task<bool> ActualizarEstadoAnalizadoSolicitudAnalisis(int idSolicitudAnalisis, int idUsuario, string? observacion);


        public Task<bool> DevolverAnalizado(int id, int idUsuario, string observacion = null);

        public Task<bool> ActualizarEstadoLegajo(int id, int idUsuario, string observacion = null);

        public Task<bool> ActualizarEstadoFinalizado(int id, int idUsuario, string observacion = null);

        public Task<List<SolicitudAnalisis>> ObtenerBandejaAnalista(int estado, DateTime? fechaInicio, DateTime? fechaFin, string? numeroUnico);
    }
}
