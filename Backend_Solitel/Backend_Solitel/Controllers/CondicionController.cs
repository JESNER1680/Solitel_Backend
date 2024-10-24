using Backend_Solitel.DTO;
using Backend_Solitel.Utility;
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
        public async Task<ActionResult<CondicionDTO>> InsertarCondicion([FromBody] CondicionDTO condicion)
        {
            try
            {
                var result = await this.gestionarCondicionBW.insertarCondicion(CondicionMapper.ToModel(condicion));
                if (result != null)
                {
                    return Ok(CondicionMapper.ToDTO(result));
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
        public async Task<ActionResult<List<CondicionDTO>>> ObtenerCondicion()
        {
            try
            {
                var condiciones = await this.gestionarCondicionBW.obtenerCondicionesTodas();
                if (condiciones != null && condiciones.Count > 0)
                {
                    return Ok(CondicionMapper.ToDTO(condiciones));
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
        [Route("{id}")]
        public async Task<ActionResult<CondicionDTO>> ObtenerCondicion(int id)
        {
            try
            {
                // Llamada al método del servicio para obtener la condición por ID
                var condicion = await this.gestionarCondicionBW.obtenerCondicionId(id);

                if (condicion != null)
                {
                    // Mapear la entidad Condicion al DTO y devolver la respuesta
                    return Ok(CondicionMapper.ToDTO(condicion));
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
        [Route("{id}")]
        public async Task<ActionResult<bool>> EliminarCondicion(int id)
        {
            try
            {
                var result = await this.gestionarCondicionBW.eliminarCondicion(id);
                if (result)
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
