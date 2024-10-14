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
    public class GestionarCondicionBW : IGestionarCondicionBW
    {
        private readonly IGestionarCondicionDA gestionarCondicionDA;

        public GestionarCondicionBW(IGestionarCondicionDA gestionarCondicionDA)
        {
            this.gestionarCondicionDA = gestionarCondicionDA;
        }

        public async Task<Condicion> eliminarCondicion(int id)
        {
            return await this.gestionarCondicionDA.eliminarCondicion(id);
        }

        public async Task<Condicion> insertarCondicion(Condicion condicion)
        {
            return await this.gestionarCondicionDA.insertarCondicion(condicion);
        }

        public async Task<List<Condicion>> obtenerCondicion()
        {
            return await this.gestionarCondicionDA.obtenerCondicion();
        }
    }
}
