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
    public class FiscaliaController : ControllerBase
    {
        private readonly IGestionarFiscaliaBW gestionarFiscaliaBW;

        public FiscaliaController(IGestionarFiscaliaBW gestionarFiscaliaBW)
        {
            this.gestionarFiscaliaBW = gestionarFiscaliaBW;
        }

        // Insertar Fiscalía
        [HttpPost]
        public async Task<ActionResult<FiscaliaDTO>> InsertarFiscalia([FromBody] FiscaliaDTO fiscalia)
        {
            try
            {
                var result = await gestionarFiscaliaBW.insertarFiscalia(fiscalia.Nombre);
                if (result != null)
                {
                    return Ok(FiscaliaMapper.ToDTO(result));
                }
                else
                {
                    return BadRequest("No se pudo insertar la fiscalía.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al insertar la fiscalía: {ex.Message}");
            }
        }

        // Obtener todas las Fiscalías
        [HttpGet]
        public async Task<ActionResult<List<FiscaliaDTO>>> ObtenerFiscalias()
        {
            try
            {
                var fiscalias = await gestionarFiscaliaBW.obtenerFiscaliasTodas();
                if (fiscalias != null && fiscalias.Count > 0)
                {
                    return Ok(FiscaliaMapper.ToDTO(fiscalias));
                }
                else
                {
                    return NotFound("No se encontraron fiscalías.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al obtener las fiscalías: {ex.Message}");
            }
        }

        // Obtener una Fiscalía por ID
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<FiscaliaDTO>> ObtenerFiscalia(int id)
        {
            try
            {
                var fiscalia = await gestionarFiscaliaBW.obtenerFiscaliaId(id);
                if (fiscalia != null)
                {
                    return Ok(FiscaliaMapper.ToDTO(fiscalia));
                }
                else
                {
                    return NotFound($"No se encontró una fiscalía con el ID {id}.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al obtener la fiscalía con ID {id}: {ex.Message}");
            }
        }

        // Eliminar (lógicamente) una Fiscalía
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<bool>> EliminarFiscalia(int id)
        {
            try
            {
                var result = await gestionarFiscaliaBW.eliminarFiscalia(id);
                if (result)
                {
                    return Ok(result); // Devuelve la fiscalía eliminada
                }
                else
                {
                    return BadRequest("No se pudo eliminar la fiscalía o no existe.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al eliminar la fiscalía: {ex.Message}");
            }
        }
    }
}
