using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.DA
{
    public interface IGestionarEstadoDA
    {
        public Task<List<Estado>> ObtenerEstados(int? idUsuario, int? idOficina);
    }
}
