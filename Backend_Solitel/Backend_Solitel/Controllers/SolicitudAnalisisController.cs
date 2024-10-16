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
        private readonly IGestionarRequerimientoAnalsisBW requerimientoAnalsisBW;
        
        public SolicitudAnalisisController(IGestionarSolicitudAnalistaBW gestionarSolicitudAnalistaBW, IGestionarRequerimientoAnalsisBW requerimientoAnalsisBW)
        {
            this.requerimientoAnalsisBW = requerimientoAnalsisBW;
            this.gestionarSolicitudAnalistaBW = gestionarSolicitudAnalistaBW;
        }
        [HttpPost]
        public async Task<bool> IngresarSolicitudAnalista(SolicitudAnalisis solicitudAnalisis)
        {
            return await this.gestionarSolicitudAnalistaBW.CrearSolicitudAnalista(solicitudAnalisis) && await this.requerimientoAnalsisBW.IngresarRequerimientoAnalisis(solicitudAnalisis.requerimentos);
        }
    }
}
