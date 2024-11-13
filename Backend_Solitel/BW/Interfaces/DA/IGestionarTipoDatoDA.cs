using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.DA
{
    public interface IGestionarTipoDatoDA
    {
        public Task<TipoDato> insertarTipoDato(TipoDato tipoDato);

        public Task<List<TipoDato>> obtenerTipoDato();

        public Task<bool> eliminarTipoDato(int id);

        public Task<TipoDato> obtenerTipoDato(int id);
    }
}
