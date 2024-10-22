using Backend_Solitel.DTO;
using Backend_Solitel.Utility;
using BC.Modelos;
using BW.Interfaces.BW;
using BW.Interfaces.DA;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Solitel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudProveedorController : ControllerBase
    {
        private readonly IGestionarSolicitudProveedorBW gestionarSolicitudProveedorBW;

        private readonly IGestionarRequerimientoProveedorBW gestionarRequerimientoProveedorBW;

        public SolicitudProveedorController(IGestionarSolicitudProveedorBW gestionarSolicitudProveedorBW, 
            IGestionarRequerimientoProveedorBW gestionarRequerimientoProveedorBW)
        {
            this.gestionarRequerimientoProveedorBW = gestionarRequerimientoProveedorBW;
            this.gestionarSolicitudProveedorBW = gestionarSolicitudProveedorBW;
        }

        [HttpPost]
        [Route("insertarSolicitudProveedor")]
        public async Task<bool> InsertarSolicitudProveedor([FromBody] SolicitudProveedorDTO solicitudProveedorDTO)
        {

            foreach (ProveedorDTO proveedorDTO in solicitudProveedorDTO.operadoras)
            {
                int idSolicitudCreada = await this.gestionarSolicitudProveedorBW
                    .InsertarSolicitudProveedor(SolicitudProveedorMapper.ToModel(solicitudProveedorDTO, proveedorDTO.TN_IdProveedor));

                if(idSolicitudCreada != 0)
                {
                    foreach (RequerimientoProveedorDTO requerimientoProveedorDTO in solicitudProveedorDTO.requerimientos)
                    {
                        bool resultadoRequerimientoInsertado = await this.gestionarRequerimientoProveedorBW
                            .InsertarRequerimientoProveedor(RequerimientoProveedorMapper.ToModel(requerimientoProveedorDTO, idSolicitudCreada));
                    }
                }
                
            }

            return true;
        }
    }
}
