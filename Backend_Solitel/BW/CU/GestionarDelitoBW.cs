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
    public class GestionarDelitoBW : IGestionarDelitoBW
    {
        private readonly IGestionarDelitoDA gestionarDelitoDA;

        public GestionarDelitoBW(IGestionarDelitoDA gestionarDelitoDA)
        {
            this.gestionarDelitoDA = gestionarDelitoDA;
        }

        public async Task<Delito> eliminarDelito(int id)
        {
            return await this.gestionarDelitoDA.eliminarDelito(id);
        }

        public async Task<Delito> insertarDelito(Delito delito)
        {
            return await this.gestionarDelitoDA.insertarDelito(delito);
        }

        public async Task<List<Delito>> obtenerDelitos()
        {
            return await this.gestionarDelitoDA.obtenerDelitos();
        }
    }
}
