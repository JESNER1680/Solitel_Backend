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
    public class GestionarTipoDatoBW : IGestionarTipoDatoBW
    {
        private readonly IGestionarTipoDatoDA gestionarTipoDatoDA;

        public GestionarTipoDatoBW(IGestionarTipoDatoDA gestionarTipoDatoDA)
        {
            this.gestionarTipoDatoDA = gestionarTipoDatoDA;
        }

        public async Task<TipoDato> eliminarTipoDato(int id)
        {
            return await this.gestionarTipoDatoDA.eliminarTipoDato(id);
        }

        public async Task<TipoDato> insertarTipoDato(TipoDato tipoDato)
        {
            return await this.gestionarTipoDatoDA.insertarTipoDato(tipoDato);
        }

        public async Task<List<TipoDato>> obtenerTipoDato()
        {
            return await this.gestionarTipoDatoDA.obtenerTipoDato();
        }

        public async Task<TipoDato> obtenerTipoDato(int id)
        {
            return await this.gestionarTipoDatoDA.obtenerTipoDato(id);
        }
    }
}
