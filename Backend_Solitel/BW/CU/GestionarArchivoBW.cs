﻿using BC.Modelos;
using BW.Interfaces.BW;
using BW.Interfaces.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.CU
{
    public class GestionarArchivoBW : IGestionarArchivoBW
    {
        private readonly IGestionarArchivoDA gestionarArchivoDA;

        public GestionarArchivoBW(IGestionarArchivoDA gestionarArchivoDA)
        {
            this.gestionarArchivoDA = gestionarArchivoDA;
        }

        public async Task<bool> InsertarArchivo_RequerimientoProveedor(Archivo archivo, int idRequerimientoProveedor)
        {
            //Reglas de Negocio - Generar el archivo hash
            return await this.gestionarArchivoDA.InsertarArchivo_RequerimientoProveedor(archivo, idRequerimientoProveedor);
        }

        public Task<Archivo> ObtenerArchivoPorIdAsync(int idArchivo)
        {
            return this.ObtenerArchivoPorIdAsync(idArchivo);
        }
    }
}