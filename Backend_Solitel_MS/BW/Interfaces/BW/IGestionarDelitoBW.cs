using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.BW
{
    public interface IGestionarDelitoBW
    {
        public Task<Delito> insertarDelito(Delito delito);

        public Task<bool> eliminarDelito(int id);

        public Task<List<Delito>> obtenerDelitosTodos();

        public Task<List<Delito>> obtenerDelitosPorCategoria(int id);

        public Task<Delito> obtenerDelitoId(int id);
    }
}
