using BC.Modelos;
using BW.Interfaces.BW;
using BW.Interfaces.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.CU
{
    public class GestionarFiscaliaBW: IGestionarFiscaliaBW
    {
        private readonly IGestionarFiscaliaDA gestionarFiscaliaDA;

        public GestionarFiscaliaBW(IGestionarFiscaliaDA gestionarFiscaliaDA)
        {
            this.gestionarFiscaliaDA = gestionarFiscaliaDA;
        }

        public async Task<bool> insertarFiscalia(string nombre)
        {

            //llamar a reglas de negocio aqui
            return await this.gestionarFiscaliaDA.insertarFiscalia(nombre);
        }

        public List<TSOLITEL_Fiscalia> obtenerFiscalias()
        {
            //llamar a reglas de negocio aqui
            return this.gestionarFiscaliaDA.obtenerFiscalias();
        }
    }
}
