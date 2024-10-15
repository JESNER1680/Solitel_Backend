using BC.Modelos;
using BW.Interfaces.BW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Solitel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubModalidadController : ControllerBase
    {
        private readonly IGestionarSubModalidadBW gestionarSubModalidadBW;

        public SubModalidadController(IGestionarSubModalidadBW gestionarSubModalidadBW)
        {
            this.gestionarSubModalidadBW = gestionarSubModalidadBW;
        }

        [HttpPost]
        [Route("insertarSubModalidad")]
        public async Task<ActionResult<SubModalidad>> InsertarSubModalidad([FromBody] SubModalidad subModalidad)
        {
            try
            {
                var result = await this.gestionarSubModalidadBW.insertarSubModalidad(subModalidad);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("No se pudo insertar la submodalidad.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al insertar la submodalidad: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("obtenerSubModalidad")]
        public async Task<ActionResult<List<SubModalidad>>> ObtenerSubModalidad()
        {
            try
            {
                var subModalidades = await this.gestionarSubModalidadBW.obtenerSubModalidad();
                if (subModalidades != null && subModalidades.Count > 0)
                {
                    return Ok(subModalidades);
                }
                else
                {
                    return NotFound("No se encontraron submodalidades.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al obtener las submodalidades: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("eliminarSubModalidad/{id}")]
        public async Task<ActionResult<SubModalidad>> EliminarSubModalidad(int id)
        {
            try
            {
                var result = await this.gestionarSubModalidadBW.eliminarSubModalidad(id);
                if (result != null)
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
    }
}
