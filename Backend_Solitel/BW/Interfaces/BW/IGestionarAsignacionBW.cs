using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.BW
{
    public interface IGestionarAsignacionBW
    {
        public Task<bool> InsertarAsignacion(int idSolicitudAnalisis, int idUsuario);
    }
}
