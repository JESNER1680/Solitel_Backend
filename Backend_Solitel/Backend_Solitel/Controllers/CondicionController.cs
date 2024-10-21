using Backend_Solitel.DTO;
using BC.Modelos;
using BW.Interfaces.BW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Solitel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CondicionController : ControllerBase
    {
        private readonly IGestionarCondicionBW gestionarCondicionBW;

        public CondicionController(IGestionarCondicionBW gestionarCondicionBW)
        {
            this.gestionarCondicionBW = gestionarCondicionBW;
        }


        // Método para insertar una nueva condición
        [HttpPost]
        [Route("insertarCondicion")]
        public async Task<ActionResult<Condicion>> InsertarCondicion([FromBody] Condicion condicion)
        {
            try
            {
                var result = await this.gestionarCondicionBW.insertarCondicion(condicion);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("No se pudo insertar la condición.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al insertar la condición: {ex.Message}");
            }
        }

        // Método para obtener la lista de condiciones
        [HttpGet]
        [Route("obtenerCondicion")]
        public async Task<ActionResult<List<Condicion>>> ObtenerCondicion()
        {
            try
            {
                var condiciones = await this.gestionarCondicionBW.obtenerCondicion();
                if (condiciones != null && condiciones.Count > 0)
                {
                    return Ok(condiciones);
                }
                else
                {
                    return NotFound("No se encontraron condiciones.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al obtener las condiciones: {ex.Message}");
            }
        }

        // Método para obtener una condición por ID
        [HttpGet]
        [Route("obtenerCondicion/{id}")]
        public async Task<ActionResult<CondicionDTO>> ObtenerCondicion(int id)
        {
            try
            {
                // Llamada al método del servicio para obtener la condición por ID
                var condicion = await this.gestionarCondicionBW.obtenerCondicion(id);

                if (condicion != null)
                {
                    // Mapear la entidad Condicion al DTO y devolver la respuesta
                    return Ok(condicion);
                }
                else
                {
                    // Si no se encuentra la condición, devolver un 404
                    return NotFound($"No se encontró una condición con el ID {id}.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al obtener la condición con ID {id}: {ex.Message}");
            }
        }

        // Método para eliminar (lógicamente) una condición
        [HttpDelete]
        [Route("eliminarCondicion/{id}")]
        public async Task<ActionResult<Condicion>> EliminarCondicion(int id)
        {
            try
            {
                var result = await this.gestionarCondicionBW.eliminarCondicion(id);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("No se pudo eliminar la condición o no existe.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al eliminar la condición: {ex.Message}");
            }
        }
    }
}
