using BC.Modelos;
using BW.Interfaces.BW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Solitel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolitelController : ControllerBase
    {
        private readonly IGestionarFiscaliaBW bw;

        public SolitelController(IGestionarFiscaliaBW bw)
        {
            this.bw = bw;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> insertarFiscalia(string nombre)
        {
            return await this.bw.insertarFiscalia(nombre);
        }

        [HttpGet]
        public List<Fiscalia> obtenerFiscalias()
        {
            return this.bw.obtenerFiscalias();
        }
    }
}
