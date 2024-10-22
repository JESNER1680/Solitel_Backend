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

        public async Task<Fiscalia> eliminarFiscalia(int id)
        {
            return await this.gestionarFiscaliaDA.eliminarFiscalia(id);
        }

        public async Task<bool> insertarFiscalia(string nombre)
        {
            return await this.gestionarFiscaliaDA.insertarFiscalia(nombre);
        }

        public async Task<Fiscalia> obtenerFiscalia(int id)
        {
            return await this.gestionarFiscaliaDA.obtenerFiscalia(id);
        }

        public async Task<List<Fiscalia>> obtenerFiscalias()
        {
            return await this.gestionarFiscaliaDA.obtenerFiscalias();
        }
    }
}
