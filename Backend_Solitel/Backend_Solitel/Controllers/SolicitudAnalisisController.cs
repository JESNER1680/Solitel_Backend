using Backend_Solitel.DTO;
using BC.Modelos;
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
            Console.WriteLine("INGRESE PARA INSERTAR LA SOLICITUD DE ANALISIS");
            return await this.gestionarSolicitudAnalistaBW.CrearSolicitudAnalista(Utility.SolicitudAnalisisMapper.ToModel(solicitudAnalisis));
        }

        [HttpGet]
        public async Task<ActionResult<List<SolicitudAnalisis>>> ConsultarSolicitudesAnalisis(
        int pageNumber = 1,
        int pageSize = 10,
        int? idEstado = null,
        string numeroUnico = null,
        DateTime? fechaInicio = null,
        DateTime? fechaFin = null,
        string caracterIngresado = null)
        {
            try
            {
                // Llamamos al método del servicio para obtener las solicitudes
                var solicitudes = await this.gestionarSolicitudAnalistaBW.ConsultarSolicitudesAnalisisAsync(
                    pageNumber,
                    pageSize,
                    idEstado,
                    numeroUnico,
                    fechaInicio,
                    fechaFin,
                    caracterIngresado);

                // Devolvemos un OK (200) con el resultado en JSON
                return Ok(solicitudes);
            }
            catch (Exception ex)
            {
                // En caso de error, devolvemos un error interno del servidor (500) con el mensaje de error
                return StatusCode(500, $"Ocurrió un error al consultar las solicitudes de análisis: {ex.Message}");
            }
        }

        [HttpGet("consultar")]
        public async Task<List<SolicitudAnalisis>> ObtenerSolicitudesAnalisisController()
        {
            return await gestionarSolicitudAnalistaBW.ObtenerSolicitudesAnalisis();
        }

        [HttpPut]
        [Route("ActualizarEstadoAnalizadoSolicitudAnalisis")]
        public async Task<IActionResult> ActualizarEstadoAnalizadoSolicitudAnalisis(int idSolicitudAnalisis, int idUsuario, string? observacion)
        {
            try
            {

                bool resultado = await this.gestionarSolicitudAnalistaBW.ActualizarEstadoAnalizadoSolicitudAnalisis(idSolicitudAnalisis, idUsuario, observacion);

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
                // Manejo de excepciones
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
