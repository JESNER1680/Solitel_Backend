﻿using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.DA
{
    public interface IGestionarSolicitudProveedorDA
    {
        public Task<List<SolicitudProveedor>> obtenerSolicitudesProveedor();

        public Task<int> InsertarSolicitudProveedor(SolicitudProveedor solicitudProveedor);

        public Task<List<string>> ListarNumerosUnicosTramitados();

        public Task<List<SolicitudProveedor>> consultarSolicitudesProveedorPorNumeroUnico(string numeroUnico);

        public Task<bool> relacionarRequerimientos(List<int> idSolicitudes, List<int> idRequerimientos);

        public Task<bool> MoverEstadoASinEfecto(int idSolicitudProveedor);

        public Task<List<SolicitudProveedor>> obtenerSolicitudesProveedorPorEstado(int pageNumber, int pageSize, int idEstado);
    }
}
