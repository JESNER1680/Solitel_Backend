using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.DA
{
    public interface IGestionarDelitoDA
    {
        public Task<Delito> insertarDelito(Delito delito);

        public Task<Delito> eliminarDelito(int id);

        public Task<List<Delito>> obtenerDelitos();

        public Task<List<Delito>> obtenerDelitosPorCategoria(int id);

        public Task<Delito> obtenerDelitos(int id);
    }
}
