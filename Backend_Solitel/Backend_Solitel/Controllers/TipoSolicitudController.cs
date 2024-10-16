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
        [Route("insertarTipoSolicitud")]
        public async Task<ActionResult<TipoSolicitud>> InsertarTipoSolicitud([FromBody] TipoSolicitud tipoSolicitud)
        {
            try
            {
                var result = await this.gestionarTipoSolicitudBW.insertarTipoSolicitud(tipoSolicitud);
                if (result != null)
                {
                    return Ok(result);
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
        [Route("obtenerTipoSolicitud")]
        public async Task<ActionResult<List<TipoSolicitud>>> ObtenerTipoSolicitud()
        {
            try
            {
                var tiposSolicitud = await this.gestionarTipoSolicitudBW.obtenerTipoSolicitud();
                if (tiposSolicitud != null && tiposSolicitud.Count > 0)
                {
                    return Ok(tiposSolicitud);
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

        [HttpDelete]
        [Route("eliminarTipoSolicitud/{id}")]
        public async Task<ActionResult<TipoSolicitud>> EliminarTipoSolicitud(int id)
        {
            try
            {
                var result = await this.gestionarTipoSolicitudBW.eliminarTipoSolicitud(id);
                if (result != null)
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
