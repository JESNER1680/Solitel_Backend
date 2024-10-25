using BC.Modelos;
using BC.Reglas_de_Negocio;
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

        public async Task<bool> eliminarFiscalia(int id)
        {
            return await this.gestionarFiscaliaDA.eliminarFiscalia(id);
        }

        public async Task<Fiscalia> insertarFiscalia(string nombre)
        {
            FiscaliaBR.ValidarNombre(nombre);
            return await this.gestionarFiscaliaDA.insertarFiscalia(nombre);
        }

        public async Task<Fiscalia> obtenerFiscaliaId(int id)
        {
            return await this.gestionarFiscaliaDA.obtenerFiscaliaId(id);
        }

        public async Task<List<Fiscalia>> obtenerFiscaliasTodas()
        {
            return await this.gestionarFiscaliaDA.obtenerFiscaliasTodas();
        }
    }
}
