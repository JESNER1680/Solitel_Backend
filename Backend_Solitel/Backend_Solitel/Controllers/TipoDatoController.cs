using Backend_Solitel.DTO;
using BC.Modelos;
using BW.Interfaces.BW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Solitel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDatoController : ControllerBase
    {
        private readonly IGestionarTipoDatoBW gestionarTipoDatoBW;

        public TipoDatoController(IGestionarTipoDatoBW gestionarTipoDatoBW)
        {
            this.gestionarTipoDatoBW = gestionarTipoDatoBW;
        }

        [HttpPost]
        [Route("insertarTipoDato")]
        public async Task<ActionResult<TipoDato>> InsertarTipoDato([FromBody] TipoDato tipoDato)
        {
            try
            {
                var result = await this.gestionarTipoDatoBW.insertarTipoDato(tipoDato);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("No se pudo insertar el tipo de dato.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al insertar el tipo de dato: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("obtenerTipoDato")]
        public async Task<ActionResult<List<TipoDato>>> ObtenerTipoDato()
        {
            try
            {
                var tiposDato = await this.gestionarTipoDatoBW.obtenerTipoDato();
                if (tiposDato != null && tiposDato.Count > 0)
                {
                    return Ok(tiposDato);
                }
                else
                {
                    return NotFound("No se encontraron tipos de dato.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los tipos de dato: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("obtenerTipoDato/{id}")]
        public async Task<ActionResult<TipoDatoDTO>> ObtenerTipoDato(int id)
        {
            try
            {
                var tipoDato = await this.gestionarTipoDatoBW.obtenerTipoDato(id);
                if (tipoDato != null)
                {
                    return Ok(tipoDato);
                }
                else
                {
                    return NotFound($"No se encontró un tipo de dato con el ID {id}.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al obtener el tipo de dato con ID {id}: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("eliminarTipoDato/{id}")]
        public async Task<ActionResult<TipoDato>> EliminarTipoDato(int id)
        {
            try
            {
                var result = await this.gestionarTipoDatoBW.eliminarTipoDato(id);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("No se pudo eliminar el tipo de dato o no existe.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el tipo de dato: {ex.Message}");
            }
        }
    }
}
