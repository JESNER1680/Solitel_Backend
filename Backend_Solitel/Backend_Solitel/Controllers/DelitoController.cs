using Backend_Solitel.DTO;
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

        // Método para obtener un delito por ID
        [HttpGet]
        [Route("obtenerDelito/{id}")]
        public async Task<ActionResult<DelitoDTO>> ObtenerDelito(int id)
        {
            try
            {
                // Llamada al método del servicio para obtener el delito por ID
                var delito = await this.gestionarDelitoBW.obtenerDelitos(id);

                if (delito != null)
                {
                    // Mapear la entidad Delito al DTO y devolver la respuesta
                    return Ok(delito);
                }
                else
                {
                    // Si no se encuentra el delito, devolver un 404
                    return NotFound($"No se encontró un delito con el ID {id}.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al obtener el delito con ID {id}: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("obtenerDelitosPorCategoria/{id}")]
        public async Task<ActionResult<List<DelitoDTO>>> ObtenerDelitosPorCategoria(int id)
        {
            try
            {
                // Llamar al método del servicio para obtener delitos por categoría
                var delitos = await this.gestionarDelitoBW.obtenerDelitosPorCategoria(id);

                // Verificar si se encontraron delitos
                if (delitos != null && delitos.Count > 0)
                {
                    return Ok(delitos);  // Mapear a DTO y retornar en la respuesta
                }
                else
                {
                    return NotFound($"No se encontraron delitos para la categoría con ID {id}.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al obtener los delitos de la categoría con ID {id}: {ex.Message}");
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
