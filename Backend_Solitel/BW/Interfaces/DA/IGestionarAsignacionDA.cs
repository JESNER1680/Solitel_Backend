using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.DA
{
    public interface IGestionarAsignacionDA
    {
        public Task<bool> InsertarAsignacion(int idSolicitudAnalisis, int idUsuario);
    }
}
