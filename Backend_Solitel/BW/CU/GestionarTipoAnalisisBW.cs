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
    public class GestionarTipoAnalisisBW : IGestionarTipoAnalisisBW
    {
        private readonly IGestionarTipoAnalisisDA gestionarTipoAnalisisDA;

        public GestionarTipoAnalisisBW(IGestionarTipoAnalisisDA gestionarTipoAnalisisDA)
        {
            this.gestionarTipoAnalisisDA = gestionarTipoAnalisisDA;
        }
        public async Task<bool> EliminarTipoAnalisis(int idTipoAnalisis)
        {
            return await this.gestionarTipoAnalisisDA.EliminarTipoAnalisis(idTipoAnalisis);
        }

        public async Task<List<TipoAnalisis>> obtenerTipoAnalisis()
        {
            return await this.gestionarTipoAnalisisDA.obtenerTipoAnalisis();
        }

        public async Task<TipoAnalisis> obtenerTipoAnalisis(int id)
        {
            return await this.gestionarTipoAnalisisDA.obtenerTipoAnalisis(id);
        }

        public async Task<TipoAnalisis> InsertarTipoAnalisis(TipoAnalisis tipoAnalisis)
        {
            TipoAnalisisBR.ValidarTipoAnalisis(tipoAnalisis);
            return await this.gestionarTipoAnalisisDA.InsertarTipoAnalisis(tipoAnalisis);
        }
    }
}
