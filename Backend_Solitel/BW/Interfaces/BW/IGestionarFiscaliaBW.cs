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
        public Task<bool> insertarFiscalia(string nombre);

        public Task<List<Fiscalia>> obtenerFiscalias();

        public Task<Fiscalia> eliminarFiscalia(int id);

        public Task<Fiscalia> obtenerFiscalia(int id);
    }
}
