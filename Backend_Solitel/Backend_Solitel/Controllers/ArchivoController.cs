using Backend_Solitel.DTO;
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
        public async Task<bool> InsertarArchivo_RequerimientoProveedor([FromForm] ArchivoDTO archivoDTO, [FromForm] IFormFile file)
        {

            if (archivoDTO == null)
            {
                Console.WriteLine("El archivo es nulo");
                return false;
            }

            Console.WriteLine(archivoDTO.TC_Nombre);
            Console.WriteLine(archivoDTO.TC_FormatoAchivo);
            Console.WriteLine(archivoDTO.TF_FechaModificacion);

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var contenido = memoryStream.ToArray(); // Aquí tienes el contenido como byte[]

                archivoDTO.TV_Contenido = contenido;
                Console.WriteLine(archivoDTO.TV_Contenido);  
            }

            return await this.gestionarArchivoBW.InsertarArchivo_RequerimientoProveedor(ArchivoMapper.ToModel(archivoDTO), 2);
        }

        [HttpGet]
        [Route("obtenerArchivoPorId")]
        public async Task<ArchivoDTO> DescargarArchivo(int id)
        {
            // Lógica para obtener el archivo de la base de datos usando el id
            var archivo = await this.gestionarArchivoBW.ObtenerArchivoPorIdAsync(id);

            if (archivo == null)
            {
                return null;
            }

            // Devolver el archivo
            return new ArchivoDTO
            {
                TN_IdArchivo = archivo.TN_IdArchivo,
                TC_Nombre = archivo.TC_Nombre,
                TC_FormatoAchivo = archivo.TC_FormatoAchivo,
                TV_Contenido = archivo.TV_Contenido,
                TF_FechaModificacion = archivo.TF_FechaModificacion
            };
        }
    }
}
