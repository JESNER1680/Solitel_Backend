﻿using BC.Modelos;
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
        public async Task<bool> CrearSolicitudAnalista(SolicitudAnalisis solicitudAnalisis)
        {
            return await this.solicitudAnalistaDA.CrearSolicitudAnalista(solicitudAnalisis);
        }
    }
}
