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
    public class GestionarSubModalidadBW : IGestionarSubModalidadBW
    {
        private readonly IGestionarSubModalidadDA gestionarSubModalidadDA;

        public GestionarSubModalidadBW(IGestionarSubModalidadDA gestionarSubModalidadDA) 
        {
            this.gestionarSubModalidadDA = gestionarSubModalidadDA;
        }

        public async Task<bool> eliminarSubModalidad(int id)
        {
            return await this.gestionarSubModalidadDA.eliminarSubModalidad(id);
        }

        public async Task<SubModalidad> insertarSubModalidad(SubModalidad SubModalidad)
        {
            SubModalidadBR.ValidarSubModalidad(SubModalidad);
            return await this.gestionarSubModalidadDA.insertarSubModalidad(SubModalidad);
        }

        public async Task<List<SubModalidad>> obtenerSubModalidad()
        {
            return await this.gestionarSubModalidadDA.obtenerSubModalidad();
        }

        public async Task<SubModalidad> obtenerSubModalidad(int id)
        {
            return await this.gestionarSubModalidadDA.obtenerSubModalidad(id);
        }

        public async Task<List<SubModalidad>> obtenerSubModalidadPorModalidad(int id)
        {
            return await this.gestionarSubModalidadDA.obtenerSubModalidadPorModalidad(id);
        }
    }
}
