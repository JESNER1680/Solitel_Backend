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
    public class GestionarCategoriaDelitoBW : IGestionarCategoriaDelitoBW
    {
        private readonly IGestionarCategoriaDelitoDA gestionarCategoriaDelitoDA;

        public GestionarCategoriaDelitoBW(IGestionarCategoriaDelitoDA gestionarCategoriaDelitoDA) 
        {
            this.gestionarCategoriaDelitoDA = gestionarCategoriaDelitoDA;
        }

        public async Task<CategoriaDelito> eliminarCategoriaDelito(int id)
        {
            return await this.gestionarCategoriaDelitoDA.eliminarCategoriaDelito(id);
        }

        public async Task<CategoriaDelito> insertarCategoriaDelito(CategoriaDelito categoriaDelito)
        {
            CategoriaDelitoBR.ValidarCategoriaDelito(categoriaDelito);
            return await this.gestionarCategoriaDelitoDA.insertarCategoriaDelito(categoriaDelito);
        }

        public async Task<List<CategoriaDelito>> obtenerCategoriaDelito()
        {
            return await this.gestionarCategoriaDelitoDA.obtenerCategoriaDelito();
        }
    }
}
