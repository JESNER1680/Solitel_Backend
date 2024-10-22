﻿using Backend_Solitel.DTO;
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
        public async Task<bool> InsertarObjetivoAnalisis(ObjetivoAnalisisDTO objetivoAnalisisDTO)
        {
            return await this.gestionarObjetivoAnalisisBW.InsertarObjetivoAnalisis(ObjetivoAnalisisMapper.ToModel(objetivoAnalisisDTO));
        }

        [HttpDelete]
        [Route("eliminarObjetivoAnalisis")]
        public async Task<bool> EliminarObjetivoAnalisis(int idObjetivoAnalisis)
        {
            return await this.gestionarObjetivoAnalisisBW.EliminarObjetivoAnalisis(idObjetivoAnalisis);
        }
    }
}