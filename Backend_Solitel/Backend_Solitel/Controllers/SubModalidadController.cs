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
    public class SubModalidadController : ControllerBase
    {
        private readonly IGestionarSubModalidadBW gestionarSubModalidadBW;

        public SubModalidadController(IGestionarSubModalidadBW gestionarSubModalidadBW)
        {
            this.gestionarSubModalidadBW = gestionarSubModalidadBW;
        }

        [HttpPost]
        public async Task<ActionResult<SubModalidadDTO>> InsertarSubModalidad([FromBody] SubModalidadDTO subModalidad)
        {
            try
            {
                var result = await this.gestionarSubModalidadBW.insertarSubModalidad(SubModalidadMapper.ToModel(subModalidad));
                if (result != null)
                {
                    return Ok(SubModalidadMapper.ToDTO(result));
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
        public async Task<ActionResult<List<SubModalidadDTO>>> ObtenerSubModalidad()
        {
            try
            {
                var subModalidades = await this.gestionarSubModalidadBW.obtenerSubModalidad();
                if (subModalidades != null && subModalidades.Count > 0)
                {
                    return Ok(SubModalidadMapper.ToDTO(subModalidades));
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

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<SubModalidadDTO>> ObtenerSubModalidad(int id)
        {
            try
            {
                var subModalidad = await this.gestionarSubModalidadBW.obtenerSubModalidad(id);
                if (subModalidad != null)
                {
                    return Ok(SubModalidadMapper.ToDTO(subModalidad));
                }
                else
                {
                    return NotFound($"No se encontró una submodalidad con el ID {id}.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al obtener la submodalidad con ID {id}: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("PorModalidad/{id}")]
        public async Task<ActionResult<List<SubModalidadDTO>>> ObtenerSubModalidadesPorModalidad(int id)
        {
            try
            {
                var subModalidades = await this.gestionarSubModalidadBW.obtenerSubModalidadPorModalidad(id);
                if (subModalidades != null && subModalidades.Count > 0)
                {
                    return Ok(SubModalidadMapper.ToDTO(subModalidades));
                }
                else
                {
                    return NotFound($"No se encontraron submodalidades para la modalidad con ID {id}.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las submodalidades para la modalidad con ID {id}: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<bool>> EliminarSubModalidad(int id)
        {
            try
            {
                var result = await this.gestionarSubModalidadBW.eliminarSubModalidad(id);
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
    }
}
