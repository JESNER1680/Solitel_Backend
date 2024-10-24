using Backend_Solitel.DTO;
using BC.Modelos;
using BW.Interfaces.BW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Solitel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SolicitudAnalisisController : ControllerBase
    {
        private readonly IGestionarSolicitudAnalistaBW gestionarSolicitudAnalistaBW;
        
        public SolicitudAnalisisController(IGestionarSolicitudAnalistaBW gestionarSolicitudAnalistaBW)
        {
            this.gestionarSolicitudAnalistaBW = gestionarSolicitudAnalistaBW;
        }
        [HttpPost]
        public async Task<bool> IngresarSolicitudAnalista(SolicitudAnalisisDTO solicitudAnalisis)
        {
            return await this.gestionarSolicitudAnalistaBW.CrearSolicitudAnalista(Utility.SolicitudAnalisisMapper.ToModel(solicitudAnalisis));
        }
    }
}
