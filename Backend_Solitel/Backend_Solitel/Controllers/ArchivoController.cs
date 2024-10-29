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

            return await this.gestionarArchivoBW.InsertarArchivo_RequerimientoProveedor(ArchivoMapper.ToModel(archivoDTO), 15);
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
                nombreArchivo = archivo.TC_Nombre, // Nombre del archivo
                contenidoArchivo = Convert.ToBase64String(archivo.TV_Contenido), // Convertir a Base64 para incluir en JSON
                tipoArchivo = archivo.TC_FormatoAchivo // Tipo de archivo, por ejemplo, "application/pdf"
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
