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
    public class GestionarObjetivoAnalisisBW : IGestionarObjetivoAnalisisBW
    {
        private readonly IGestionarObjetivoAnalisisDA gestionarObjetivoAnalisisDA;

        public GestionarObjetivoAnalisisBW(IGestionarObjetivoAnalisisDA gestionarObjetivoAnalisisDA)
        {
            this.gestionarObjetivoAnalisisDA = gestionarObjetivoAnalisisDA;
        }

        public async Task<bool> EliminarObjetivoAnalisis(int idObjetivoAnalisis)
        {
            return await this.gestionarObjetivoAnalisisDA.EliminarObjetivoAnalisis(idObjetivoAnalisis);
        }

        public async Task<ObjetivoAnalisis> InsertarObjetivoAnalisis(ObjetivoAnalisis objetivoAnalisis)
        {
            ObjetivoAnalisisBR.ValidarObjetivoAnalisis(objetivoAnalisis);
            return await this.gestionarObjetivoAnalisisDA.InsertarObjetivoAnalisis(objetivoAnalisis);
        }
        public async Task<List<ObjetivoAnalisis>> ObtenerObjetivoAnalisis(int idObjetivoAnalisis)
        {
            return await this.gestionarObjetivoAnalisisDA.ObtenerObjetivoAnalisis(idObjetivoAnalisis);
        }
    }
}
