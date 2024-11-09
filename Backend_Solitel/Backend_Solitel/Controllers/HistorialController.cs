using Backend_Solitel.DTO;
using Backend_Solitel.Utility;
using BC.Modelos;
using BW.Interfaces.BW;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Solitel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialController : Controller
    {
        private readonly IGestionarHistorialBW gestionarHistorialBW;

        public HistorialController(IGestionarHistorialBW gestionarHistorialBW)
        {
            this.gestionarHistorialBW = gestionarHistorialBW;
        }

        [HttpGet]
        public async Task<List<HistorialDTO>> ConsultarHistoricoDeSolicitudProveedor(int idSolicitudProveedor)
        {
            return HistorialMapper.ToDTO(await this.gestionarHistorialBW.ConsultarHistorialDeSolicitudProveedor(idSolicitudProveedor));
        }

        [HttpGet("Analisis")]
        public async Task<List<HistorialDTO>> ConsultarHistorialDeSolicitudAnalisis(int idSolicitudProveedor)
        {
            return HistorialMapper.ToDTO(await this.gestionarHistorialBW.ConsultarHistorialDeSolicitudAnalisis(idSolicitudProveedor));
        }
    }
}
