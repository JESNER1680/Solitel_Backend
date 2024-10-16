using BC.Modelos;
using BW.Interfaces.BW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Solitel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequerimientoAnalisisController : ControllerBase
    {
        private readonly IGestionarRequerimientoAnalsisBW requerimientoAnalsisBW;
        public RequerimientoAnalisisController(IGestionarRequerimientoAnalsisBW requerimientoAnalsisBW)
        {
            this.requerimientoAnalsisBW = requerimientoAnalsisBW;
        }

        [HttpPut]
        public async Task<bool> EditarRequerimientoAnalisis(RequerimentoAnalisis requerimentoAnalisis) {
            return await this.requerimientoAnalsisBW.EditarRequerimientoAnalisis(requerimentoAnalisis);
        }

        [HttpDelete]
        public async Task<bool> EliminarRequerimientoAnalisis(int idRequerimiento) {
            return await this.requerimientoAnalsisBW.EliminarRequerimientoAnalisis(idRequerimiento);
        }
    }
}
