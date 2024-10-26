using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.DA
{
    public interface IGestionarSubModalidadDA
    {
        public Task<SubModalidad> insertarSubModalidad(SubModalidad SubModalidad);

        public Task<bool> eliminarSubModalidad(int id);

        public Task<List<SubModalidad>> obtenerSubModalidad();

        public Task<List<SubModalidad>> obtenerSubModalidadPorModalidad(int id);

        public Task<SubModalidad> obtenerSubModalidad(int id);
    }
}
