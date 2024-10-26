using Backend_Solitel.DTO;
using Backend_Solitel.Utility;
using BW.Interfaces.BW;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Solitel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjetivoAnalisisController : ControllerBase
    {
        private readonly IGestionarObjetivoAnalisisBW gestionarObjetivoAnalisisBW;

        public ObjetivoAnalisisController(IGestionarObjetivoAnalisisBW gestionarObjetivoAnalisisBW)
        {
            this.gestionarObjetivoAnalisisBW = gestionarObjetivoAnalisisBW;
        }

        [HttpPost]
        [Route("insertarObjetivoAnalisis")]
        public async Task<ObjetivoAnalisisDTO> InsertarObjetivoAnalisis(ObjetivoAnalisisDTO objetivoAnalisisDTO)
        {
            var resultado = await this.gestionarObjetivoAnalisisBW.InsertarObjetivoAnalisis(ObjetivoAnalisisMapper.ToModel(objetivoAnalisisDTO));
            return ObjetivoAnalisisMapper.ToDTO(resultado);
        }

        [HttpDelete]
        [Route("eliminarObjetivoAnalisis")]
        public async Task<bool> EliminarObjetivoAnalisis(int idObjetivoAnalisis)
        {
            return await this.gestionarObjetivoAnalisisBW.EliminarObjetivoAnalisis(idObjetivoAnalisis);
        }
        [HttpGet]
        [Route("obtenerObjetivoAnalisis")]
        public async Task<List<ObjetivoAnalisisDTO>>ObtenerObjetivoAnalisis(int idObtenerObjetivoAnalisis){
            var ObjetivosObtenidos = await this.gestionarObjetivoAnalisisBW.ObtenerObjetivoAnalisis(idObtenerObjetivoAnalisis);
            return Utility.ObjetivoAnalisisMapper.ToDTOS(ObjetivosObtenidos);
        }
    }
}
