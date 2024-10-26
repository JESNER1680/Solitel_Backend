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
    public class GestionarTipoSolicitudBW : IGestionarTipoSolicitudBW
    {
        private readonly IGestionarTipoSolicitudDA gestionarTipoSolicitudDA;

        public GestionarTipoSolicitudBW(IGestionarTipoSolicitudDA gestionarTipoSolicitudDA)
        {
            this.gestionarTipoSolicitudDA = gestionarTipoSolicitudDA;
        }

        public async Task<bool> eliminarTipoSolicitud(int id)
        {
            return await this.gestionarTipoSolicitudDA.eliminarTipoSolicitud(id);
        }

        public async Task<TipoSolicitud> insertarTipoSolicitud(TipoSolicitud tipoSolicitud)
        {
            return await this.gestionarTipoSolicitudDA.insertarTipoSolicitud(tipoSolicitud);
        }

        public async Task<List<TipoSolicitud>> obtenerTipoSolicitud()
        {
            return await this.gestionarTipoSolicitudDA.obtenerTipoSolicitud();
        }

        public async Task<TipoSolicitud> obtenerTipoSolicitud(int id)
        {
            return await this.gestionarTipoSolicitudDA.obtenerTipoSolicitud(id);
        }
    }
}
