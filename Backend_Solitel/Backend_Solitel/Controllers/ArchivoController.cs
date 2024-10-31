﻿using Backend_Solitel.DTO;
using Backend_Solitel.Utility;
using BW.Interfaces.BW;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Solitel.Controllers
{
    public class ArchivoController : Controller
    {
        private readonly IGestionarArchivoBW gestionarArchivoBW;

        public ArchivoController(IGestionarArchivoBW gestionarArchivoBW)
        {
            this.gestionarArchivoBW = gestionarArchivoBW;
        }

        [HttpPost]
        [Route("insertarArchivo_RequerimientoProveedor")]
        public async Task<bool> InsertarArchivo_RequerimientoProveedor(
                                                                        [FromForm] ArchivoDTO archivoDTO,
                                                                        [FromForm] IFormFile file,
                                                                        [FromForm] int idRequerimiento)
        {
            // Verificar si archivoDTO es nulo
            if (archivoDTO == null)
            {
                return false;
            }

            // Verificar si el archivo es nulo
            if (file == null)
            {
                return false;
            }

            // Verificar si idRequerimiento es nulo o no válido
            if (idRequerimiento <= 0)
            {
                return false;
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var contenido = memoryStream.ToArray(); // Aquí tienes el contenido como byte[]

                archivoDTO.Contenido = contenido; // Asignar el contenido del archivo
                archivoDTO.FechaModificacion = DateTime.Now; // Asignar la fecha de modificación actual
            }

            // Llamada al método de negocio para guardar el archivo y el idRequerimiento
            return await this.gestionarArchivoBW.InsertarArchivo_RequerimientoProveedor(ArchivoMapper.ToModel(archivoDTO), idRequerimiento);
        }


        [HttpGet]
        [Route("obtenerArchivoPorId")]
        public async Task<IActionResult> DescargarArchivo(int id)
        {
            // Lógica para obtener el archivo de la base de datos usando el id
            var archivo = await this.gestionarArchivoBW.ObtenerArchivoPorIdAsync(id);

            if (archivo == null)
            {
                return NotFound();
            }

            // En lugar de solo devolver el archivo, envolvemos la respuesta en un objeto JSON
            var archivoResultado = new
            {
                nombreArchivo = archivo.Nombre, // Nombre del archivo
                contenidoArchivo = Convert.ToBase64String(archivo.Contenido), // Convertir a Base64 para incluir en JSON
                tipoArchivo = archivo.FormatoArchivo // Tipo de archivo, por ejemplo, "application/pdf"
            };

            return Ok(archivoResultado);
        }


        [HttpGet]
        [Route("obtenerArchivosDeSolicitudesProveedor")]
        public async Task<IActionResult> obtenerArchivosDeSolicitudesProveedor(List<int> idSolicitudes)
        {
            return null;
        }

    }
}
