﻿using Backend_Solitel.DTO;
using Backend_Solitel.Utility;
using BC.Modelos;
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


        [Route("api/obtenerArchivosDeSolicitudesProveedor")]
        [HttpGet]
        public async Task<List<Archivo>> obtenerArchivosDeSolicitudesProveedor([FromQuery] List<int> idSolicitudes)
        {
            return await this.gestionarArchivoBW.ObtenerArchivosDeSolicitudesProveedor(idSolicitudes);
        }

        [HttpGet("ObtenerArchivosDeSolicitud")]
        public async Task<ActionResult<List<Archivo>>> ObtenerArchivosDeSolicitud(int id)
        {
            try
            {
                var archivos = await this.gestionarArchivoBW.ObtenerArchivosDeSolicitudesProveedor(id);

                if (archivos == null || archivos.Count == 0)
                {
                    return NotFound("No se encontraron archivos para la solicitud especificada.");
                }

                return Ok(archivos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al obtener archivos de la solicitud: {ex.Message}");
            }
        }

        [HttpGet("ObtenerArchivosPorSolicitudAnalisis")]
        public async Task<ActionResult<List<Archivo>>> ObtenerArchivosPorSolicitudAnalisis(int idSolicitudAnalisis)
        {
            try
            {
                var archivos = await this.gestionarArchivoBW.ObtenerArchivosPorSolicitudAnalisis(idSolicitudAnalisis, "DefaultTipo");

                if (archivos == null || archivos.Count == 0)
                {
                    return NotFound("No se encontraron archivos para la solicitud especificada.");
                }

                return Ok(archivos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al obtener archivos de la solicitud: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("InsertarArchivoRespuestaSolicitudAnalisis")]
        public async Task<bool> InsertarArchivoRespuestaSolicitudAnalisis([FromForm] ArchivoDTO archivoDTO,
                                                                        [FromForm] IFormFile file,
                                                                        [FromForm] int idSolicitudAnalisis)
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

            // Verificar si idSolicitudAnalisis es nulo
            if (idSolicitudAnalisis <= 0)
            {
                return false;
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var contenido = memoryStream.ToArray(); // Contenido como byte[]

                archivoDTO.Contenido = contenido;
                archivoDTO.FechaModificacion = DateTime.Now;
            }

            return await this.gestionarArchivoBW.InsertarArchivoRespuestaSolicitudAnalisis(ArchivoMapper.ToModel(archivoDTO), idSolicitudAnalisis, "Respuesta");
        }

        [HttpPost]
        [Route("InsertarArchivoInformeFinalSolicitudAnalisis")]
        public async Task<bool> InsertarArchivoInformeFinalSolicitudAnalisis([FromForm] ArchivoDTO archivoDTO,
                                                                        [FromForm] IFormFile file,
                                                                        [FromForm] int idSolicitudAnalisis)
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

            // Verificar si idSolicitudAnalisis es nulo
            if (idSolicitudAnalisis <= 0)
            {
                return false;
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var contenido = memoryStream.ToArray(); // Contenido como byte[]

                archivoDTO.Contenido = contenido;
                archivoDTO.FechaModificacion = DateTime.Now;
            }

            return await this.gestionarArchivoBW.InsertarArchivoRespuestaSolicitudAnalisis(ArchivoMapper.ToModel(archivoDTO), idSolicitudAnalisis, "Informe");
        }

        [HttpGet]
        [Route("ObtenerArchivosRespuestaSolicitudAnalisis")]
        public async Task<ActionResult<List<Archivo>>> ObtenerArchivosRespuestaSolicitudAnalisis(int idSolicitudAnalisis)
        {
            try
            {
                var archivos = await this.gestionarArchivoBW.ObtenerArchivosPorSolicitudAnalisis(idSolicitudAnalisis, "Respuesta");

                if (archivos == null || archivos.Count == 0)
                {
                    return NotFound("No se encontraron archivos para la solicitud especificada.");
                }

                return Ok(archivos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al obtener archivos de la solicitud: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("ObtenerArchivosInformeFinalSolicitudAnalisis")]
        public async Task<ActionResult<List<Archivo>>> ObtenerArchivosInformeFinalSolicitudAnalisis(int idSolicitudAnalisis)
        {
            try
            {
                var archivos = await this.gestionarArchivoBW.ObtenerArchivosPorSolicitudAnalisis(idSolicitudAnalisis, "Informe");

                if (archivos == null || archivos.Count == 0)
                {
                    return NotFound("No se encontraron archivos para la solicitud especificada.");
                }

                return Ok(archivos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al obtener archivos de la solicitud: {ex.Message}");
            }
        }



    }
}
