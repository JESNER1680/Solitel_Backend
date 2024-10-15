using BC.Modelos;
using BW.Interfaces.BW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Solitel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModalidadController : ControllerBase
    {
        private readonly IGestionarModalidadBW gestionarModalidadBW;

        public ModalidadController(IGestionarModalidadBW gestionarModalidadBW)
        {
            this.gestionarModalidadBW = gestionarModalidadBW;
        }

        [HttpPost]
        [Route("insertarModalidad")]
        public async Task<ActionResult<Modalidad>> InsertarModalidad([FromBody] Modalidad modalidad)
        {
            try
            {
                var result = await this.gestionarModalidadBW.insertarModalidad(modalidad);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("No se pudo insertar la modalidad.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al insertar la modalidad: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("obtenerModalidad")]
        public async Task<ActionResult<List<Modalidad>>> ObtenerModalidad()
        {
            try
            {
                var modalidades = await this.gestionarModalidadBW.obtenerModalidad();
                if (modalidades != null && modalidades.Count > 0)
                {
                    return Ok(modalidades);
                }
                else
                {
                    return NotFound("No se encontraron modalidades.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al obtener las modalidades: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("eliminarModalidad/{id}")]
        public async Task<ActionResult<Modalidad>> EliminarModalidad(int id)
        {
            try
            {
                var result = await this.gestionarModalidadBW.eliminarModalidad(id);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("No se pudo eliminar la modalidad o no existe.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al eliminar la modalidad: {ex.Message}");
            }
        }
    }
}
