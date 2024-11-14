using Backend_Solitel.DTO;
using Backend_Solitel.Utility;
using BC.Modelos;
using BW.Interfaces.BW;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Solitel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OficinaController : ControllerBase
    {
        private readonly IGestionarOficinaBW gestionarOficinaBW;

        public OficinaController(IGestionarOficinaBW gestionarOficinaBW)
        {
            this.gestionarOficinaBW = gestionarOficinaBW;
        }

        [HttpGet]
        [Route("ObtenerOficinas")]
        public async Task<ActionResult<List<OficinaDTO>>> ObtenerOficinas(string? tipo)
        {
            try
            {
                var oficinas = await this.gestionarOficinaBW.ObtenerOficinas(tipo);

                return OficinaMapper.ToDTO(oficinas);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("consultarOficina/{idOficina}")]
        public async Task<OficinaDTO> ConsultarOficina(int idOficina)
        {
            var oficinaDTOs = await this.gestionarOficinaBW.ConsultarOficina(idOficina);
            return OficinaMapper.ToDTO(oficinaDTOs);
        }

        [HttpPost]
        [Route("insertarOficina")]
        public async Task<bool> InsertarOficina(OficinaDTO oficinaDTO)
        {
            return await this.gestionarOficinaBW.InsertarOficina(OficinaMapper.ToModel(oficinaDTO));
        }

        [HttpDelete]
        [Route("eliminarOficina/{idOficina}")]
        public async Task<bool> EliminarOficina(int idOficina)
        {
            return await this.gestionarOficinaBW.EliminarOficina(idOficina);
        }
    }
}
