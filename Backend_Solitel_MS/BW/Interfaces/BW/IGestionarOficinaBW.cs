using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.BW
{
    public interface IGestionarOficinaBW
    {
        public Task<bool> InsertarOficina(Oficina oficina);

        public Task<List<Oficina>> ConsultarOficinas();

        public Task<bool> EliminarOficina(int idOficina);
    }
}
