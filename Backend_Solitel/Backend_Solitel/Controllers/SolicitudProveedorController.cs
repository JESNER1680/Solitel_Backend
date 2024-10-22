using Backend_Solitel.DTO;
using Backend_Solitel.Utility;
using BC.Modelos;
using BW.Interfaces.BW;
using BW.Interfaces.DA;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Backend_Solitel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudProveedorController : ControllerBase
    {
        private readonly IGestionarSolicitudProveedorBW gestionarSolicitudProveedorBW;

        private readonly IGestionarRequerimientoProveedorBW gestionarRequerimientoProveedorBW;

        public SolicitudProveedorController(IGestionarSolicitudProveedorBW gestionarSolicitudProveedorBW,
            IGestionarRequerimientoProveedorBW gestionarRequerimientoProveedorBW)
        {
            this.gestionarRequerimientoProveedorBW = gestionarRequerimientoProveedorBW;
            this.gestionarSolicitudProveedorBW = gestionarSolicitudProveedorBW;
        }

        [HttpPost]
        [Route("insertarSolicitudProveedor")]
        public async Task<bool> InsertarSolicitudProveedor([FromBody] SolicitudProveedorDTO solicitudProveedorDTO)
        {

            foreach (ProveedorDTO proveedorDTO in solicitudProveedorDTO.Operadoras)
            {
                int idSolicitudCreada = await this.gestionarSolicitudProveedorBW
                    .InsertarSolicitudProveedor(SolicitudProveedorMapper.ToModel(solicitudProveedorDTO, proveedorDTO.TN_IdProveedor));

                if (idSolicitudCreada != 0)
                {
                    foreach (RequerimientoProveedorDTO requerimientoProveedorDTO in solicitudProveedorDTO.Requerimientos)
                    {
                        bool resultadoRequerimientoInsertado = await this.gestionarRequerimientoProveedorBW
                            .InsertarRequerimientoProveedor(RequerimientoProveedorMapper.ToModel(requerimientoProveedorDTO, idSolicitudCreada));
                    }
                }

            }

            return true;
        }

        [HttpGet]
        [Route("consultarSolicitudesProveedor")]
        public async Task<List<SolicitudProveedorDTO>> ConsultarSolicitudesProveedor(int pageNumber, int pageSize)
        {
            var solicitudesProveedor = SolicitudProveedorMapper.ToDTO(await this.gestionarSolicitudProveedorBW.obtenerSolicitudesProveedor(pageNumber, pageSize));

            foreach (SolicitudProveedorDTO solicitudProveedorDTO in solicitudesProveedor)
            {
                solicitudProveedorDTO.Requerimientos = RequerimientoProveedorMapper
                    .ToDTO(await this.gestionarRequerimientoProveedorBW.ConsultarRequerimientosProveedor(solicitudProveedorDTO.IdSolicitudProveedor), solicitudProveedorDTO.IdSolicitudProveedor);

                foreach (RequerimientoProveedorDTO requerimientoProveedorDTO in solicitudProveedorDTO.Requerimientos)
                {
                    requerimientoProveedorDTO.datosRequeridos = DatoRequeridoMapper.ToDTO(await this.gestionarRequerimientoProveedorBW.ConsultarDatosRequeridos(requerimientoProveedorDTO.TN_IdRequerimientoProveedor));

                    requerimientoProveedorDTO.tipoSolicitudes = TipoSolicitudMapper.ToDTO(await this.gestionarRequerimientoProveedorBW.ConsultarTipoSolicitudes(requerimientoProveedorDTO.TN_IdRequerimientoProveedor));
                }
            }
            return solicitudesProveedor;
        }

        [HttpGet]
        [Route("listarNumerosUnicosTramitados")]
        public async Task<List<int>> ListarNumerosUnicosTramitados()
        {
            return await this.gestionarSolicitudProveedorBW.ListarNumerosUnicosTramitados();
        }

        [HttpGet]
        [Route("consultarSolicitudesProveedorPorNumeroUnico")]
        public async Task<List<SolicitudFiltradaProveedorDTO>> ConsultarSolicitudesProveedorPorNumeroUnico(int numeroUnico)
        {
            List<SolicitudProveedor> solicitudes = await this.gestionarSolicitudProveedorBW.consultarSolicitudesProveedorPorNumeroUnico(numeroUnico);
            return SolicitudProveedorMapper.FiltrarListaSolicitudesProveedor(solicitudes);
        }
    }
}
