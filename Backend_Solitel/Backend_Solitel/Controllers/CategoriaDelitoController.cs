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
    public class CategoriaDelitoController : ControllerBase
    {
        private readonly IGestionarCategoriaDelitoBW gestionarCategoriaDelitoBW;

        public CategoriaDelitoController (IGestionarCategoriaDelitoBW gestionarCategoriaDelitoBW)
        {
            this.gestionarCategoriaDelitoBW = gestionarCategoriaDelitoBW;
        }

        // Método para insertar una nueva categoría de delito
        [HttpPost]
        [Route("insertarCategoriaDelito")]
        public async Task<ActionResult<CategoriaDelitoDTO>> InsertarCategoriaDelito(CategoriaDelitoDTO categoriaDelito)
        {
            try
            {
                var result = await this.gestionarCategoriaDelitoBW.
                    insertarCategoriaDelito(CategoriaDelitoMapper.ToModel(categoriaDelito));
                if (result != null)
                {
                    return Ok(CategoriaDelitoMapper.ToDTO(result));
                }
                else
                {
                    return BadRequest("No se pudo insertar la categoría de delito.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al insertar la categoría de delito: {ex.Message}");
            }
        }

        // Método para obtener la lista de categorías de delito
        [HttpGet]
        [Route("obtenerCategoriaDelito")]
        public async Task<ActionResult<List<CategoriaDelitoDTO>>> ObtenerCategoriaDelito()
        {
            try
            {
                var categorias = await this.gestionarCategoriaDelitoBW.obtenerCategoriaDelito();
                if (categorias != null && categorias.Count > 0)
                {
                    return Ok(CategoriaDelitoMapper.ToDTO(categorias));
                }
                else
                {
                    return NotFound("No se encontraron categorías de delito.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al obtener las categorías de delito: {ex.Message}");
            }
        }

        // Método para obtener una única categoría de delito por ID
        [HttpGet]
        [Route("obtenerCategoriaDelito/{id}")]
        public async Task<ActionResult<CategoriaDelitoDTO>> ObtenerCategoriaDelito(int id)
        {
            try
            {
                // Llama al método para obtener una categoría por ID
                var categoria = await this.gestionarCategoriaDelitoBW.obtenerCategoriaDelito(id);

                // Verifica si la categoría fue encontrada
                if (categoria != null)
                {
                    // Mapea la entidad a DTO y la retorna con el código 200 (OK)
                    return Ok(CategoriaDelitoMapper.ToDTO(categoria));
                }
                else
                {
                    // Si no se encontró la categoría, devuelve 404 (NotFound)
                    return NotFound($"No se encontró una categoría de delito con ID {id}.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al obtener la categoría de delito: {ex.Message}");
            }
        }

        // Método para eliminar (lógicamente) una categoría de delito
        [HttpDelete]
        [Route("eliminarCategoriaDelito/{id}")]
        public async Task<ActionResult<CategoriaDelitoDTO>> EliminarCategoriaDelito(int id)
        {
            try
            {
                var result = await this.gestionarCategoriaDelitoBW.eliminarCategoriaDelito(id);
                if (result != null)
                {
                    return Ok(CategoriaDelitoMapper.ToDTO(result));
                }
                else
                {
                    return BadRequest("No se pudo eliminar la categoría de delito o no existe.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al eliminar la categoría de delito: {ex.Message}");
            }
        }

    }
}
