using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.DA
{
    public interface IGestionarCondicionDA
    {
        public Task<Condicion> insertarCondicion(Condicion condicion);

        public Task<Condicion> eliminarCondicion(int id);

        public Task<List<Condicion>> obtenerCondicion();
    }
}
