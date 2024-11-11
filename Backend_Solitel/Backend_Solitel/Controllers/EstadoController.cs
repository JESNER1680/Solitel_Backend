using Backend_Solitel.DTO;
using Backend_Solitel.Utility;
using BW.Interfaces.BW;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Solitel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly IGestionarEstadoBW gestionarEstadoBW;

        public EstadoController(IGestionarEstadoBW gestionarEstadoBW)
        {
            this.gestionarEstadoBW = gestionarEstadoBW;
        }

        [HttpGet]
        public async Task<List<EstadoDTO>> ObtenerEstados(int? idUsuario, int? idOficina)
        {
            return EstadoMapper.ToDTO(await this.gestionarEstadoBW.ObtenerEstados(idUsuario, idOficina));
        }
    }
}
