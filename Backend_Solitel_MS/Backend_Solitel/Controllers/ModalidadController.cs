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
    public class ModalidadController : ControllerBase
    {
        private readonly IGestionarModalidadBW gestionarModalidadBW;

        public ModalidadController(IGestionarModalidadBW gestionarModalidadBW)
        {
            this.gestionarModalidadBW = gestionarModalidadBW;
        }

        [HttpPost]
        public async Task<ActionResult<ModalidadDTO>> InsertarModalidad([FromBody] ModalidadDTO modalidad)
        {
            try
            {
                var result = await this.gestionarModalidadBW.insertarModalidad(ModalidadMapper.ToModel(modalidad));
                if (result != null)
                {
                    return Ok(ModalidadMapper.ToDTO(result));
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
        public async Task<ActionResult<List<ModalidadDTO>>> ObtenerModalidad()
        {
            try
            {
                var modalidades = await this.gestionarModalidadBW.obtenerModalidad();
                if (modalidades != null && modalidades.Count > 0)
                {
                    return Ok(ModalidadMapper.ToDTO(modalidades));
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

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ModalidadDTO>> ObtenerModalidad(int id)
        {
            try
            {
                // Llamar al método del servicio para obtener la modalidad por ID
                var modalidad = await this.gestionarModalidadBW.obtenerModalidad(id);

                // Verificar si se encontró la modalidad
                if (modalidad != null)
                {
                    return Ok(ModalidadMapper.ToDTO(modalidad));  // Mapear a DTO y retornar en la respuesta
                }
                else
                {
                    return NotFound($"No se encontró una modalidad con el ID {id}.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al obtener la modalidad con ID {id}: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<bool>> EliminarModalidad(int id)
        {
            try
            {
                var result = await this.gestionarModalidadBW.eliminarModalidad(id);
                if (result)
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
