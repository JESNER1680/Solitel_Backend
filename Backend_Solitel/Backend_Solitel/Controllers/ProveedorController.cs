using Backend_Solitel.DTO;
using Backend_Solitel.Utility;
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
        [Route("insertarProveedor")]
        public async Task<bool> InsertarProveedor(ProveedorDTO proveedorDTO)
        {
            return await this.gestionarProveedorBW.InsertarProveedor(ProveedorMapper.ToModel(proveedorDTO));
        }

        [HttpGet]
        [Route("consultarProveedores")]
        public async Task<List<ProveedorDTO>> ConsultarProveedores()
        {
            var proveedoresDTOs = await this.gestionarProveedorBW.ConsultarProveedores();
            return ProveedorMapper.ToDTO(proveedoresDTOs);
        }

        [HttpDelete]
        [Route("eliminarProveedor/{idProveedor}")]
        public async Task<bool> EliminarProveedor(int idProveedor)
        {
            return await this.gestionarProveedorBW.EliminarProveedor(idProveedor);
        }
    }
}
