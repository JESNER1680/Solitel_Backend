using Backend_Solitel.DTO;
using Backend_Solitel.Utility;
using BC.Modelos;
using BC.Reglas_de_Negocio;
using BW.Interfaces.BW;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Solitel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAnalisisController : ControllerBase
    {
        private readonly IGestionarTipoAnalisisBW gestionarTipoAnalisisBW;

        public TipoAnalisisController(IGestionarTipoAnalisisBW gestionarTipoAnalisisBW)
        {
            this.gestionarTipoAnalisisBW = gestionarTipoAnalisisBW;
        }

        [HttpPost]
        public async Task<ActionResult<TipoAnalisisDTO>> InsertarTipoAnalisis([FromBody] TipoAnalisisDTO tipoAnalisisDTO)
        {
            try
            {
                var tipoAnalisis = await this.gestionarTipoAnalisisBW.InsertarTipoAnalisis(TipoAnalisisMapper.ToModel(tipoAnalisisDTO));

                if (tipoAnalisis != null)
                {
                    return Ok(TipoAnalisisMapper.ToDTO(tipoAnalisis));
                }
                else
                {
                    return BadRequest("Error al insertar el tipo de análisis.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<bool>> EliminarTipoAnalisis(int id)
        {
            try
            {
                var result = await this.gestionarTipoAnalisisBW.EliminarTipoAnalisis(id);
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("No se pudo eliminar la submodalidad o no existe.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al eliminar la submodalidad: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoAnalisisDTO>>> ObtenerTipoAnalisis()
        {
            try
            {
                var tipoAnalisisList = await this.gestionarTipoAnalisisBW.obtenerTipoAnalisis();
                if (tipoAnalisisList != null && tipoAnalisisList.Count > 0)
                {
                    return Ok(TipoAnalisisMapper.ToDTO(tipoAnalisisList));
                }
                else
                {
                    return NotFound("No se encontraron tipos de analisis.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TipoAnalisisDTO>> ObtenerTipoAnalisis(int id)
        {
            try
            {
                var tipoAnalisis = await this.gestionarTipoAnalisisBW.obtenerTipoAnalisis(id);

                if (tipoAnalisis != null)
                {
                    var tipoAnalisisDTO = TipoAnalisisMapper.ToDTO(tipoAnalisis);
                    return Ok(tipoAnalisisDTO);
                }
                else
                {
                    return NotFound($"No se encontró un tipo de análisis con el ID {id}.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

    }
}
