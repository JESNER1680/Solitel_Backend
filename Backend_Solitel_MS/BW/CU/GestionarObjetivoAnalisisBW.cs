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
    public class GestionarObjetivoAnalisisBW : IGestionarObjetivoAnalisisBW
    {
        private readonly IGestionarObjetivoAnalisisDA gestionarObjetivoAnalisisDA;

        public GestionarObjetivoAnalisisBW(IGestionarObjetivoAnalisisDA gestionarObjetivoAnalisisDA)
        {
            this.gestionarObjetivoAnalisisDA = gestionarObjetivoAnalisisDA;
        }

        public async Task<bool> EliminarObjetivoAnalisis(int idObjetivoAnalisis)
        {
            //Reglas de Negocio
            return await this.gestionarObjetivoAnalisisDA.EliminarObjetivoAnalisis(idObjetivoAnalisis);
        }

        public async Task<bool> InsertarObjetivoAnalisis(ObjetivoAnalisis objetivoAnalisis)
        {
            //Reglas de Negocio
            return await this.gestionarObjetivoAnalisisDA.InsertarObjetivoAnalisis(objetivoAnalisis);
        }
    }
}
