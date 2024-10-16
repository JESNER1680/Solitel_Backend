using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.BW
{
    public interface IGestionarCategoriaDelitoBW
    {
        public Task<CategoriaDelito> insertarCategoriaDelito(CategoriaDelito categoriaDelito);

        public Task<CategoriaDelito> eliminarCategoriaDelito(int id);

        public Task<List<CategoriaDelito>> obtenerCategoriaDelito();
    }
}
