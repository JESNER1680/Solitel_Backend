using BC.Modelos;
using BW.Interfaces.BW;
using BW.Interfaces.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.CU
{
    public class GestionarSolicitudAnalistaBW : IGestionarSolicitudAnalistaBW
    {
        private readonly IGestionarSolicitudAnalistaDA solicitudAnalistaDA;
        public GestionarSolicitudAnalistaBW(IGestionarSolicitudAnalistaDA _solicitudAnalistaDA)
        {
            this.solicitudAnalistaDA = _solicitudAnalistaDA;
        }

        public async Task<bool> ActualizarEstadoAnalizadoSolicitudAnalisis(int idSolicitudAnalisis, int idUsuario, string? observacion)
        {
            return await this.solicitudAnalistaDA.ActualizarEstadoAnalizadoSolicitudAnalisis(idSolicitudAnalisis, idUsuario, observacion);
        }

        public async Task<bool> ActualizarEstadoFinalizado(int id, int idUsuario, string observacion = null)
        {
            return await this.solicitudAnalistaDA.ActualizarEstadoFinalizado(id, idUsuario, observacion);
        }

        public async Task<List<SolicitudAnalisis>> ConsultarSolicitudesAnalisisAsync(int pageNumber, int pageSize, int? idEstado = null, string numeroUnico = null, DateTime? fechaInicio = null, DateTime? fechaFin = null, string caracterIngresado = null)
        {
            return await this.solicitudAnalistaDA.ConsultarSolicitudesAnalisisAsync(pageNumber, pageSize, idEstado, numeroUnico, fechaInicio, fechaFin, caracterIngresado);         
        }

        public async Task<bool> CrearSolicitudAnalista(SolicitudAnalisis solicitudAnalisis)
        {
            return await this.solicitudAnalistaDA.CrearSolicitudAnalista(solicitudAnalisis);
        }

        public async Task<bool> DevolverAnalizado(int id, int idUsuario, string observacion = null)
        {
            return await this.solicitudAnalistaDA.DevolverAnalizado(id, idUsuario, observacion);
        }

        public async Task<List<SolicitudAnalisis>> ObtenerSolicitudesAnalisis()
        {
            return await this.solicitudAnalistaDA.ObtenerSolicitudesAnalisis();
        }

        public async Task<bool> ActualizarEstadoLegajo(int id, int idUsuario, string observacion = null)
        {
            return await this.solicitudAnalistaDA.ActualizarEstadoLegajo(id, idUsuario, observacion);
        }
    }
}
