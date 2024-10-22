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
    public class GestionarTipoAnalisisBW : IGestionarTipoAnalisisBW
    {
        private readonly IGestionarTipoAnalisisDA gestionarTipoAnalisisDA;

        public GestionarTipoAnalisisBW(IGestionarTipoAnalisisDA gestionarTipoAnalisisDA)
        {
            this.gestionarTipoAnalisisDA = gestionarTipoAnalisisDA;
        }
        public async Task<bool> EliminarTipoAnalisis(int idTipoAnalisis)
        {
            //Reglas de Negocio
            return await this.gestionarTipoAnalisisDA.EliminarTipoAnalisis(idTipoAnalisis);
        }

        public async Task<bool> InsertarTipoAnalisis(TipoAnalisis tipoAnalisis)
        {
            //Reglas de Negocio
            return await this.gestionarTipoAnalisisDA.InsertarTipoAnalisis(tipoAnalisis);
        }
    }
}
