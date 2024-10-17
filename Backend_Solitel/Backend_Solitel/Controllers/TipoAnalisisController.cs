using Backend_Solitel.DTO;
using Backend_Solitel.Utility;
using BW.Interfaces.BW;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Solitel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAnalisisController : ControllerBase
    {
        private readonly IGestionarTipoAnalisisBW gestionarTipoAnalisisBW;

        public TipoAnalisisController(IGestionarTipoAnalisisBW gestionarTipoAnalisisBW)
        {
            this.gestionarTipoAnalisisBW = gestionarTipoAnalisisBW;
        }

        [HttpPost]
        [Route("insertarTipoAnalisis")]
        public async Task<bool> InsertarTipoAnalisis(TipoAnalisisDTO tipoAnalisisDTO)
        {
            return await this.gestionarTipoAnalisisBW.InsertarTipoAnalisis(TipoAnalisisMapper.ToModel(tipoAnalisisDTO));
        }

        [HttpPut]
        [Route("eliminarTipoAnalisis")]
        public async Task<bool> EliminarTipoAnalisis(int idTipoAnalisis)
        {
            return await this.gestionarTipoAnalisisBW.EliminarTipoAnalisis(idTipoAnalisis);
        }
    }
}
