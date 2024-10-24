using Backend_Solitel.DTO;
using Backend_Solitel.Utility;
using BC.Modelos;
using BW.Interfaces.BW;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Solitel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IGestionarProveedorBW gestionarProveedorBW;

        public ProveedorController(IGestionarProveedorBW gestionarProveedorBW)
        {
            this.gestionarProveedorBW = gestionarProveedorBW;
        }

        [HttpPost]
        public async Task<ActionResult<ProveedorDTO>> InsertarProveedor(ProveedorDTO proveedorDTO)
        {
            try
            {
                var result = await this.gestionarProveedorBW.InsertarProveedor(ProveedorMapper.ToModel(proveedorDTO));
                if (result != null)
                {
                    return Ok(ProveedorMapper.ToDTO(result));
                }
                else
                {
                    return BadRequest("No se pudo insertar el proveedor.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al insertar el proveedor: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<ProveedorDTO>>> ConsultarProveedores()
        {
            try
            {
                var proveedores = await this.gestionarProveedorBW.ConsultarProveedores();
                if (proveedores != null && proveedores.Count > 0)
                {
                    return Ok(ProveedorMapper.ToDTO(proveedores));
                }
                else
                {
                    return NotFound("No se encontraron proveedores.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al obtener los proveedores: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<bool>> EliminarProveedor(int id)
        {
            try
            {
                var result = await this.gestionarProveedorBW.EliminarProveedor(id);
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("No se pudo eliminar el proveedor o no existe.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al eliminar el proveedor: {ex.Message}");
            }
        }
    }
}
