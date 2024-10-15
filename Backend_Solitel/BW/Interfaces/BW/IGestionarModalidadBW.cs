using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.BW
{
    public interface IGestionarModalidadBW
    {
        public Task<Modalidad> insertarModalidad(Modalidad modalidad);

        public Task<Modalidad> eliminarModalidad(int id);

        public Task<List<Modalidad>> obtenerModalidad();
    }
}
