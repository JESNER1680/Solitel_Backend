using BW.Interfaces.BW;
using BW.Interfaces.DA;

namespace BW.CU
{
    public class GestionarAsignacionBW : IGestionarAsignacionBW
    {
        private readonly IGestionarAsignacionDA gestionarAsignacionDA;

        public GestionarAsignacionBW(IGestionarAsignacionDA gestionarAsignacionDA)
        {
            this.gestionarAsignacionDA = gestionarAsignacionDA;
        }

        public async Task<bool> InsertarAsignacion(int idSolicitudAnalisis, int idUsuario)
        {
            return await this.gestionarAsignacionDA.InsertarAsignacion(idSolicitudAnalisis, idUsuario);
        }
    }
}
