﻿using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.DA
{
    public interface IGestionarTipoSolicitudDA
    {

        public Task<TipoSolicitud> insertarTipoSolicitud(TipoSolicitud tipoSolicitud);
         
        public Task<List<TipoSolicitud>> obtenerTipoSolicitud();

        public Task<bool> eliminarTipoSolicitud(int id);

        public Task<TipoSolicitud> obtenerTipoSolicitud(int id);
    }
}
