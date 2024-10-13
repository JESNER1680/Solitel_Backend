using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.DA
{
    public interface IGestionarFiscaliaDA
    {
        public Task<bool> insertarFiscalia(string nombre);

        public List<TSOLITEL_Fiscalia> obtenerFiscalias();
    }
}
