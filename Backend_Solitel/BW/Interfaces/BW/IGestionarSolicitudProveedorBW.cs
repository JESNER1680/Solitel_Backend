using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.BW
{
    public interface IGestionarSolicitudProveedorBW
    {
        public Task<int> InsertarSolicitudProveedor(SolicitudProveedor solicitudProveedor);

        public Task<List<SolicitudProveedor>> obtenerSolicitudesProveedor(int pageNumber, int pageSize);

        public Task<List<string>> ListarNumerosUnicosTramitados();

        public Task<List<SolicitudProveedor>> consultarSolicitudesProveedorPorNumeroUnico(string numeroUnico);

        public Task<bool> relacionarRequerimientos(List<int> idSolicitudes, List<int> idRequerimientos);

        public Task<bool> MoverEstadoASinEfecto(int idSolicitudProveedor);

        public Task<List<SolicitudProveedor>> obtenerSolicitudesProveedorPorEstado(int pageNumber, int pageSize, int idEstado);
    }
}
