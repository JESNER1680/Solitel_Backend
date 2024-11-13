using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.BW
{
    public interface IGestionarEstadoBW
    {
        public Task<List<Estado>> ObtenerEstados(int? idUsuario, int? idOficina);
    }
}
