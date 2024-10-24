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
    public class TipoSolicitudController : ControllerBase
    {
        private readonly IGestionarTipoSolicitudBW gestionarTipoSolicitudBW;

        public TipoSolicitudController(IGestionarTipoSolicitudBW gestionarTipoSolicitudBW)
        {
            this.gestionarTipoSolicitudBW = gestionarTipoSolicitudBW;
        }

        [HttpPost]
        public async Task<ActionResult<TipoSolicitudDTO>> InsertarTipoSolicitud([FromBody] TipoSolicitudDTO tipoSolicitud)
        {
            try
            {
                var result = await this.gestionarTipoSolicitudBW.insertarTipoSolicitud(TipoSolicitudMapper.ToModel(tipoSolicitud));
                if (result != null)
                {
                    return Ok(TipoSolicitudMapper.ToDTO(result));
                }
                else
                {
                    return BadRequest("No se pudo insertar el tipo de solicitud.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al insertar el tipo de solicitud: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoSolicitudDTO>>> ObtenerTipoSolicitud()
        {
            try
            {
                var tiposSolicitud = await this.gestionarTipoSolicitudBW.obtenerTipoSolicitud();
                if (tiposSolicitud != null && tiposSolicitud.Count > 0)
                {
                    return Ok(TipoSolicitudMapper.ToDTO(tiposSolicitud));
                }
                else
                {
                    return NotFound("No se encontraron tipos de solicitud.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los tipos de solicitud: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TipoSolicitudDTO>> ObtenerTipoSolicitud(int id)
        {
            try
            {
                var tipoSolicitud = await this.gestionarTipoSolicitudBW.obtenerTipoSolicitud(id);
                if (tipoSolicitud != null)
                {
                    return Ok(TipoSolicitudMapper.ToDTO(tipoSolicitud));
                }
                else
                {
                    return NotFound($"No se encontró un tipo de solicitud con el ID {id}.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al obtener el tipo de solicitud con ID {id}: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<TipoSolicitud>> EliminarTipoSolicitud(int id)
        {
            try
            {
                var result = await this.gestionarTipoSolicitudBW.eliminarTipoSolicitud(id);
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("No se pudo eliminar el tipo de solicitud o no existe.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el tipo de solicitud: {ex.Message}");
            }
        }
    }
}
