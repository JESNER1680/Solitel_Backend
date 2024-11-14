using BW.Interfaces.BW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Backend_Solitel.Controllers
{
    public class AsignacionController : ControllerBase
    {
        private readonly IGestionarAsignacionBW gestionarAsignacionBW;

        public AsignacionController(IGestionarAsignacionBW gestionarAsignacionBW)
        {
            this.gestionarAsignacionBW = gestionarAsignacionBW;
        }

        [HttpPost]
        [Route("asignarUsuarioAnalista")]
        public async Task<IActionResult> InsertarAsignacion(int idSolicitudAnalisis, int idUsuario)
        {
            bool insertExitoso = await this.gestionarAsignacionBW.InsertarAsignacion(idSolicitudAnalisis, idUsuario);

            if (insertExitoso)
            {
                return Ok(new { success = true, message = "Usuario asignado correctamente" });
            }
            else
            {
                return BadRequest(new { success = false, message = "No se logró realizar la asignación" });
            }
        }
    }
}
