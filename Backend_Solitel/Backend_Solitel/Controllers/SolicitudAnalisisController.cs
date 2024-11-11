using Backend_Solitel.DTO;
using BC.Modelos;
using BW.CU;
using BW.Interfaces.BW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Solitel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SolicitudAnalisisController : ControllerBase
    {
        private readonly IGestionarSolicitudAnalistaBW gestionarSolicitudAnalistaBW;

        public SolicitudAnalisisController(IGestionarSolicitudAnalistaBW gestionarSolicitudAnalistaBW)
        {
            this.gestionarSolicitudAnalistaBW = gestionarSolicitudAnalistaBW;
        }

        [HttpPost]
        public async Task<bool> IngresarSolicitudAnalista(SolicitudAnalisisDTO solicitudAnalisis)
        {
            return await this.gestionarSolicitudAnalistaBW.CrearSolicitudAnalista(Utility.SolicitudAnalisisMapper.ToModel(solicitudAnalisis));
        }

        [HttpGet("consultar")]
        public async Task<List<SolicitudAnalisis>> ObtenerSolicitudesAnalisisBandejaInvestigador(int? idEstado, DateTime? fechainicio, DateTime? fechaFin, string? numeroUnico, int? idOficina, int? idUsuario, int? idSolicitud)
        {
            return await gestionarSolicitudAnalistaBW.ObtenerSolicitudesAnalisis(idEstado, fechainicio, fechaFin, numeroUnico, idOficina, idUsuario, idSolicitud);
        }

        [HttpGet("obtenerBandejaAnalista")]
        public async Task<ActionResult<List<SolicitudAnalisis>>> ObtenerSolicitudesAnalisisBandejaAnalista(int estado, DateTime? fechaInicio, DateTime? fechaFin, string? numeroUnico)
        {
            try
            {
                var result = await gestionarSolicitudAnalistaBW.ObtenerBandejaAnalista(estado, fechaInicio, fechaFin, numeroUnico);

                if (result == null || !result.Any())
                {
                    return NotFound("No se encontraron solicitudes de análisis para los criterios proporcionados.");
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                // Manejo de errores de validación de argumentos
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                return StatusCode(500, new { message = "Ocurrió un error al obtener las solicitudes de análisis.", details = ex.Message });
            }
        }

        [HttpPut("ActualizarEstadoAnalizadoSolicitudAnalisis")]
        public async Task<IActionResult> ActualizarEstadoAnalizadoSolicitudAnalisis(int idSolicitudAnalisis, int idUsuario, string? observacion)
        {
            try
            {
                bool resultado = await gestionarSolicitudAnalistaBW.ActualizarEstadoAnalizadoSolicitudAnalisis(idSolicitudAnalisis, idUsuario, observacion);

                if (resultado)
                {
                    return Ok(new { mensaje = "Estado actualizado a Analizado correctamente." });
                }
                else
                {
                    return BadRequest(new { mensaje = "No se pudo actualizar el estado de la solicitud." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Ocurrió un error al actualizar el estado a Analizado: {ex.Message}" });
            }
        }

        [HttpPut("devolverAnalizado")]
        public async Task<IActionResult> DevolverAnalizado([FromQuery] int id, [FromQuery] int idUsuario, [FromQuery] string observacion = null)
        {
            try
            {
                var result = await this.gestionarSolicitudAnalistaBW.DevolverAnalizado(id, idUsuario, observacion);

                if (result)
                {
                    return Ok(new { message = "La solicitud ha sido devuelta a Analizado exitosamente." });
                }
                else
                {
                    return BadRequest(new { message = "No se pudo devolver la solicitud a Analizado." });
                }
            }
            catch (System.Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, new { message = $"Ocurrió un error al intentar devolver la solicitud a Tramitado: {ex.Message}" });
            }
        }
        
        [HttpPut("AprobarSolicitudAnalisis")]
        public async Task<IActionResult> AprobarSolicitudAnalisis(int idSolicitudAnalisis, int idUsuario, string? observacion)
        {
            try
            {
                Console.WriteLine("CAPA CONTROLLER");
                Console.WriteLine(idSolicitudAnalisis + " " + idUsuario + " " + observacion);
                bool resultado = await gestionarSolicitudAnalistaBW.AprobarSolicitudAnalisis(idSolicitudAnalisis,idUsuario, observacion);
                Console.WriteLine("BOOLEAN CONFIRMACION");
                Console.WriteLine(idSolicitudAnalisis + " " + idUsuario + " " + observacion);
                if (resultado)
                {
                    return Ok(new { mensaje = "Estado actualizado a Analizado correctamente." });
                }
                else
                {
                    return BadRequest(new { mensaje = "No se pudo actualizar el estado de la solicitud." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Ocurrió un error al actualizar el estado a Analizado: {ex.Message}" });
            }
        }

        [HttpPut("actualizarEstadoLegajo")]
        public async Task<IActionResult> ActualizarEstadoLegajo(int id, int idUsuario, [FromQuery] string observacion = null)
        {
            try
            {
                bool resultado = await this.gestionarSolicitudAnalistaBW.ActualizarEstadoLegajo(id, idUsuario, observacion);

                if (resultado)
                {
                    return Ok(new { mensaje = "Estado actualizado a Legajo correctamente." });
                }
                else
                {
                    return BadRequest(new { mensaje = "No se pudo actualizar el estado de la solicitud." });
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return StatusCode(500, new { mensaje = $"Ocurrió un error al actualizar el estado a Legajo: {ex.Message}" });
            }
        }

        [HttpPut("actualizarEstadoFinalizado")]
        public async Task<IActionResult> ActualizarEstadoFinalizado(int id, int idUsuario, [FromQuery] string observacion = null)
        {
            try
            {
                bool resultado = await this.gestionarSolicitudAnalistaBW.ActualizarEstadoFinalizado(id, idUsuario, observacion);

                if (resultado)
                {
                    return Ok(new { mensaje = "Estado actualizado a Finalizado correctamente." });
                }
                else
                {
                    return BadRequest(new { mensaje = "No se pudo actualizar el estado de la solicitud." });
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return StatusCode(500, new { mensaje = $"Ocurrió un error al actualizar el estado a Finalizado: {ex.Message}" });
            }
        }
    }
}
