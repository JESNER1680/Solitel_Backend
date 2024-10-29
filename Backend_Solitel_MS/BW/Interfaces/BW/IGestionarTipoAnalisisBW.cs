using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.BW
{
    public interface IGestionarTipoAnalisisBW
    {
        public Task<TipoAnalisis> InsertarTipoAnalisis(TipoAnalisis tipoAnalisis);

        public Task<bool> EliminarTipoAnalisis(int idTipoAnalisis);

        public Task<List<TipoAnalisis>> obtenerTipoAnalisis();

        public Task<TipoAnalisis> obtenerTipoAnalisis(int id);
    }
}
