using BC.Modelos;
using BW.Interfaces.BW;
using BW.Interfaces.DA;

namespace BW.CU
{
    public class GestionarHistorialBW : IGestionarHistorialBW
    {
        private readonly IGestionarHistorialDA gestionarHistorialDA;

        public GestionarHistorialBW(IGestionarHistorialDA gestionarHistorialDA)
        {
            this.gestionarHistorialDA = gestionarHistorialDA;
        }

        public async Task<List<Historial>> ConsultarHistorialDeSolicitudProveedor(int idSolicitudProveedor)
        {
            return await this.gestionarHistorialDA.ConsultarHistorialDeSolicitudProveedor(idSolicitudProveedor);
        }
    }
}
