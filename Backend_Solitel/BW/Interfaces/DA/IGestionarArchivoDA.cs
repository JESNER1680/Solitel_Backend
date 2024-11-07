﻿using BC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Interfaces.DA
{
    public interface IGestionarArchivoDA
    {
        public Task<bool> InsertarArchivo_RequerimientoProveedor(Archivo archivo, int idRequerimientoProveedor);

        public Task<Archivo> ObtenerArchivoPorIdAsync(int idArchivo);

        public Task<List<Archivo>> ObtenerArchivosDeSolicitudesProveedor(List<int> idsSolicitudesProveedor);

        public Task<List<Archivo>> ObtenerArchivosDeSolicitudesProveedor(int id);

        public Task<List<Archivo>> ObtenerArchivosPorSolicitudAnalisis(int idSolicitudAnalisis);

        public Task<bool> InsertarArchivoRespuestaSolicitudAnalisis(Archivo archivo, int idSolicitudAnalisis);
    }
}
