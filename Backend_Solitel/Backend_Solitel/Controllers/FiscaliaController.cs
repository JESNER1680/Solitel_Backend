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
        [Route("insertarFiscalia")]
        public async Task<ActionResult<bool>> InsertarFiscalia([FromBody] Fiscalia fiscalia)
        {
            try
            {
                var result = await gestionarFiscaliaBW.insertarFiscalia(fiscalia.TC_Nombre);
                if (result)
                {
                    return Ok(result);
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
        [Route("obtenerFiscalia")]
        public async Task<ActionResult<List<Fiscalia>>> ObtenerFiscalias()
        {
            try
            {
                var fiscalias = await gestionarFiscaliaBW.obtenerFiscalias();
                if (fiscalias != null && fiscalias.Count > 0)
                {
                    return Ok(fiscalias);
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
        [Route("obtenerFiscalia/{id}")]
        public async Task<ActionResult<Fiscalia>> ObtenerFiscalia(int id)
        {
            try
            {
                var fiscalia = await gestionarFiscaliaBW.obtenerFiscalia(id);
                if (fiscalia != null)
                {
                    return Ok(fiscalia);
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
        [Route("eliminarFiscalia/{id}")]
        public async Task<ActionResult<Fiscalia>> EliminarFiscalia(int id)
        {
            try
            {
                var result = await gestionarFiscaliaBW.eliminarFiscalia(id);
                if (result != null)
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
