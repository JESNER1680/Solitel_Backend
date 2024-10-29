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
    public class GestionarEstadoBW : IGestionarEstadoBW
    {
        private readonly IGestionarEstadoDA gestionarEstadoDA;

        public GestionarEstadoBW(IGestionarEstadoDA gestionarEstadoDA)
        {
            this.gestionarEstadoDA = gestionarEstadoDA;
        }
        public Task<List<Estado>> ObtenerEstados()
        {
            return this.gestionarEstadoDA.ObtenerEstados();
        }
    }
}
