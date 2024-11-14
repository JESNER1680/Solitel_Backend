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
    public class GestionarOficinaBW : IGestionarOficinaBW
    {
        private readonly IGestionarOficinaDA gestionarOficinaDA;

        public GestionarOficinaBW(IGestionarOficinaDA gestionarOficinaDA)
        {
            this.gestionarOficinaDA = gestionarOficinaDA;
        }

        public async Task<Oficina> ConsultarOficina(int idOficina)
        {
            return await this.gestionarOficinaDA.ConsultarOficina(idOficina);
        }

        public async Task<List<Oficina>> ObtenerOficinas(string? tipo)
        {
            //Reglas de Negocio
            return await this.gestionarOficinaDA.ObtenerOficinas(tipo);
        }

        public async Task<bool> EliminarOficina(int idProveedor)
        {
            //Reglas de Negocio
            return await this.gestionarOficinaDA.EliminarOficina(idProveedor);
        }

        public async Task<bool> InsertarOficina(Oficina oficina)
        {
            //Reglas de Negocio
            return await this.gestionarOficinaDA.InsertarOficina(oficina);
        }
    }
}
