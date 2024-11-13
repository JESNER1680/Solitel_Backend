using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.DA
{
    public interface IGestionarOficinaDA
    {
        public Task<bool> InsertarOficina(Oficina oficina);

        public Task<List<Oficina>> ConsultarOficinas();

        public Task<Oficina> ConsultarOficina(int idOficina);

        public Task<bool> EliminarOficina(int idOficina);
    }
}
