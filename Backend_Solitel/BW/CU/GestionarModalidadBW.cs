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
    public class GestionarModalidadBW: IGestionarModalidadBW 
    {
        private readonly IGestionarModalidadDA gestionarModalidadDA;

        public GestionarModalidadBW(IGestionarModalidadDA gestionarModalidadDA)
        {
            this.gestionarModalidadDA = gestionarModalidadDA;
        }

        public async Task<Modalidad> eliminarModalidad(int id)
        {
            return await this.gestionarModalidadDA.eliminarModalidad(id);
        }

        public async Task<Modalidad> insertarModalidad(Modalidad modalidad)
        {
            return await this.gestionarModalidadDA.insertarModalidad(modalidad);
        }

        public async Task<List<Modalidad>> obtenerModalidad()
        {
            return await this.gestionarModalidadDA.obtenerModalidad();
        }

        public async Task<Modalidad> obtenerModalidad(int id)
        {
            return await this.gestionarModalidadDA.obtenerModalidad(id);
        }
    }
}
