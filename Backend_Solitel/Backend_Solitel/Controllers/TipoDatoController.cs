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
    public class TipoDatoController : ControllerBase
    {
        private readonly IGestionarTipoDatoBW gestionarTipoDatoBW;

        public TipoDatoController(IGestionarTipoDatoBW gestionarTipoDatoBW)
        {
            this.gestionarTipoDatoBW = gestionarTipoDatoBW;
        }

        [HttpPost]
        public async Task<ActionResult<TipoDatoDTO>> InsertarTipoDato([FromBody] TipoDatoDTO tipoDato)
        {
            try
            {
                var result = await this.gestionarTipoDatoBW.insertarTipoDato(TipoDatoMapper.ToModel(tipoDato));
                if (result != null)
                {
                    return Ok(TipoDatoMapper.ToDTO(result));
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
        public async Task<ActionResult<List<TipoDatoDTO>>> ObtenerTipoDato()
        {
            try
            {
                var tiposDato = await this.gestionarTipoDatoBW.obtenerTipoDato();
                if (tiposDato != null && tiposDato.Count > 0)
                {
                    return Ok(TipoDatoMapper.ToDTO(tiposDato));
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
        [Route("{id}")]
        public async Task<ActionResult<TipoDatoDTO>> ObtenerTipoDato(int id)
        {
            try
            {
                var tipoDato = await this.gestionarTipoDatoBW.obtenerTipoDato(id);
                if (tipoDato != null)
                {
                    return Ok(TipoDatoMapper.ToDTO(tipoDato));
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
        [Route("{id}")]
        public async Task<ActionResult<bool>> EliminarTipoDato(int id)
        {
            try
            {
                var result = await this.gestionarTipoDatoBW.eliminarTipoDato(id);
                if (result)
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
