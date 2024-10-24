using BC.Modelos;
using BC.Reglas_de_Negocio;
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

        public async Task<bool> eliminarDelito(int id)
        {
            return await this.gestionarDelitoDA.eliminarDelito(id);
        }

        public async Task<Delito> insertarDelito(Delito delito)
        {
            DelitoBR.ValidarDelito(delito);
            return await this.gestionarDelitoDA.insertarDelito(delito);
        }

        public async Task<List<Delito>> obtenerDelitosTodos()
        {
            return await this.gestionarDelitoDA.obtenerDelitosTodos();
        }

        public async Task<Delito> obtenerDelitoId(int id)
        {
            return await this.gestionarDelitoDA.obtenerDelitoId(id);
        }

        public async Task<List<Delito>> obtenerDelitosPorCategoria(int id)
        {
            return await this.gestionarDelitoDA.obtenerDelitosPorCategoria(id);
        }
    }
}
