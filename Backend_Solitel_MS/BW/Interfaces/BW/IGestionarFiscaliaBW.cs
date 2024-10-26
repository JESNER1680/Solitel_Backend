using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.BW
{
    public interface IGestionarFiscaliaBW
    {
        public Task<Fiscalia> insertarFiscalia(string nombre);

        public Task<List<Fiscalia>> obtenerFiscaliasTodas();

        public Task<bool> eliminarFiscalia(int id);

        public Task<Fiscalia> obtenerFiscaliaId(int id);
    }
}
