using BC.Modelos;
using BW.Interfaces.BW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Solitel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DelitoController : ControllerBase
    {
        private readonly IGestionarDelitoBW gestionarDelitoBW;

        public DelitoController(IGestionarDelitoBW gestionarDelitoBW)
        {
            this.gestionarDelitoBW = gestionarDelitoBW;
        }

        // Método para insertar un nuevo delito
        [HttpPost]
        [Route("insertarDelito")]
        public async Task<ActionResult<Delito>> InsertarDelito([FromBody] Delito delito)
        {
            try
            {
                var result = await this.gestionarDelitoBW.insertarDelito(delito);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("No se pudo insertar el delito.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al insertar el delito: {ex.Message}");
            }
        }

        // Método para obtener la lista de delitos
        [HttpGet]
        [Route("obtenerDelitos")]
        public async Task<ActionResult<List<Delito>>> ObtenerDelitos()
        {
            try
            {
                var delitos = await this.gestionarDelitoBW.obtenerDelitos();
                if (delitos != null && delitos.Count > 0)
                {
                    return Ok(delitos);
                }
                else
                {
                    return NotFound("No se encontraron delitos.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al obtener los delitos: {ex.Message}");
            }
        }

        // Método para eliminar un delito (eliminación lógica)
        [HttpDelete]
        [Route("eliminarDelito/{id}")]
        public async Task<ActionResult<Delito>> EliminarDelito(int id)
        {
            try
            {
                var result = await this.gestionarDelitoBW.eliminarDelito(id);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("No se pudo eliminar el delito o el delito no existe.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al eliminar el delito: {ex.Message}");
            }
        }
    }
}
