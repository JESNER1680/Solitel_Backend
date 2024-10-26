using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.BW
{
    public interface IGestionarObjetivoAnalisisBW
    {
        public Task<ObjetivoAnalisis> InsertarObjetivoAnalisis(ObjetivoAnalisis objetivoAnalisis);

        public Task<bool> EliminarObjetivoAnalisis(int idObjetivoAnalisis);

        public Task<List<ObjetivoAnalisis>> ObtenerObjetivoAnalisis(int idObjetivoAnalisis);
    }
}
