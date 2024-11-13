using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.BW
{
    public interface IGestionarCondicionBW
    {
        public Task<Condicion> insertarCondicion(Condicion condicion);

        public Task<bool> eliminarCondicion(int id);

        public Task<List<Condicion>> obtenerCondicionesTodas();

        public Task<Condicion> obtenerCondicionId(int id);
    }
}
