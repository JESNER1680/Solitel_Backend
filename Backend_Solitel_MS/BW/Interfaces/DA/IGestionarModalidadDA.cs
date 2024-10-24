using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.DA
{
    public interface IGestionarModalidadDA
    {
        public Task<Modalidad> insertarModalidad(Modalidad modalidad);

        public Task<bool> eliminarModalidad(int id);

        public Task<List<Modalidad>> obtenerModalidad();

        public Task<Modalidad> obtenerModalidad(int id);
    }
}
