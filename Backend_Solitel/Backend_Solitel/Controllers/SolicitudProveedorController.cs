﻿using Backend_Solitel.DTO;
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

            List<int> idListaSolicitudesCreadas = new List<int>();

            List<int> idRequerimientosCreados = new List<int>();

            foreach (ProveedorDTO proveedorDTO in solicitudProveedorDTO.Operadoras)
            {
                int idSolicitudCreada = await this.gestionarSolicitudProveedorBW
                    .InsertarSolicitudProveedor(SolicitudProveedorMapper.ToModel(solicitudProveedorDTO, proveedorDTO.IdProveedor));

                if (idSolicitudCreada != 0)
                {
                    idListaSolicitudesCreadas.Add(idSolicitudCreada);
                }
                else
                {
                    return false;
                }

            }

            foreach (RequerimientoProveedorDTO requerimientoProveedorDTO in solicitudProveedorDTO.Requerimientos)
            {
                int idRequerimientoInsertado = await this.gestionarRequerimientoProveedorBW
                    .InsertarRequerimientoProveedor(RequerimientoProveedorMapper.ToModel(requerimientoProveedorDTO));

                if (idRequerimientoInsertado != 0)
                {
                    idRequerimientosCreados.Add(idRequerimientoInsertado);
                }
                else
                {
                    return false;
                }
            }

            bool resultadoRelacion = await this.gestionarSolicitudProveedorBW.relacionarRequerimientos(idListaSolicitudesCreadas, idRequerimientosCreados);

            return resultadoRelacion;
        }

        [HttpGet]
        [Route("consultarSolicitudesProveedor")]
        public async Task<List<SolicitudProveedorDTO>> ConsultarSolicitudesProveedor()
        {
            var solicitudesProveedor = SolicitudProveedorMapper.ToDTO(await this.gestionarSolicitudProveedorBW.obtenerSolicitudesProveedor());

            foreach (SolicitudProveedorDTO solicitudProveedorDTO in solicitudesProveedor)
            {

                solicitudProveedorDTO.Requerimientos = RequerimientoProveedorMapper
                    .ToDTO(await this.gestionarRequerimientoProveedorBW.ConsultarRequerimientosProveedor(solicitudProveedorDTO.IdSolicitudProveedor), solicitudProveedorDTO.IdSolicitudProveedor);

                foreach (RequerimientoProveedorDTO requerimientoProveedorDTO in solicitudProveedorDTO.Requerimientos)
                {
                    requerimientoProveedorDTO.datosRequeridos = DatoRequeridoMapper.ToDTO(await this.gestionarRequerimientoProveedorBW.ConsultarDatosRequeridos(requerimientoProveedorDTO.IdRequerimientoProveedor));

                    requerimientoProveedorDTO.tipoSolicitudes = TipoSolicitudMapper.ToDTO(await this.gestionarRequerimientoProveedorBW.ConsultarTipoSolicitudes(requerimientoProveedorDTO.IdRequerimientoProveedor));

                }
            }

            return solicitudesProveedor;
        }

        [HttpGet]
        [Route("listarNumerosUnicosTramitados")]
        public async Task<List<string>> ListarNumerosUnicosTramitados()
        {
            return await this.gestionarSolicitudProveedorBW.ListarNumerosUnicosTramitados();
        }

        [HttpGet]
        [Route("consultarSolicitudesProveedorPorNumeroUnico")]
        public async Task<List<SolicitudFiltradaProveedorDTO>> ConsultarSolicitudesProveedorPorNumeroUnico(string numeroUnico)
        {
            List<SolicitudProveedor> solicitudes = await this.gestionarSolicitudProveedorBW.consultarSolicitudesProveedorPorNumeroUnico(numeroUnico);
            return SolicitudProveedorMapper.FiltrarListaSolicitudesProveedor(solicitudes);
        }

        [HttpPut]
        [Route("moverEstadoASinEfecto")]
        public async Task<IActionResult> MoverEstadoASinEfecto(int idSolicitudProveedor, int idUsuario, string? observacion)
        {

            try
            {
                var resultado = await this.gestionarSolicitudProveedorBW.MoverEstadoASinEfecto(idSolicitudProveedor, idUsuario, observacion);

                if (resultado)
                {
                    return Ok(new { mensaje = "Estado actualizado a Sin Efecto correctamente." });
                }
                else
                {
                    return BadRequest(new { mensaje = "No se pudo actualizar el estado de la solicitud." });
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return StatusCode(500, new { mensaje = $"Ocurrió un error al actualizar el estado a Sin Efecto: {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("obtenerSolicitudesProveedorPorEstado")]
        public async Task<List<SolicitudProveedor>> ObtenerSolicitudesProveedorPorEstado(int pageNumber, int pageSize, int idEstado)
        {
            return await this.gestionarSolicitudProveedorBW.obtenerSolicitudesProveedorPorEstado(pageNumber, pageSize, idEstado);
        }

        [HttpPut("actualizarEstadoFinalizado/{id}/{idUsuario}")]
        public async Task<IActionResult> ActualizarEstadoFinalizado(int id, int idUsuario, [FromQuery] string observacion = null)
        {
            try
            {
                bool resultado = await this.gestionarSolicitudProveedorBW.ActualizarEstadoFinalizado(id, idUsuario, observacion);

                if (resultado)
                {
                    return Ok(new { mensaje = "Estado actualizado a Finalizado correctamente." });
                }
                else
                {
                    return BadRequest(new { mensaje = "No se pudo actualizar el estado de la solicitud." });
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return StatusCode(500, new { mensaje = $"Ocurrió un error al actualizar el estado a Finalizado: {ex.Message}" });
            }
        }

        [HttpPut("actualizarEstadoLegajo/{id}/{idUsuario}")]
        public async Task<IActionResult> ActualizarEstadoLegajo(int id, int idUsuario, [FromQuery] string observacion = null)
        {
            try
            {
                bool resultado = await this.gestionarSolicitudProveedorBW.ActualizarEstadoLegajo(id, idUsuario, observacion);

                if (resultado)
                {
                    return Ok(new { mensaje = "Estado actualizado a Legajo correctamente." });
                }
                else
                {
                    return BadRequest(new { mensaje = "No se pudo actualizar el estado de la solicitud." });
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return StatusCode(500, new { mensaje = $"Ocurrió un error al actualizar el estado a Legajo: {ex.Message}" });
            }
        }

        //[HttpGet("{idSolicitud}")]
        //public async Task<ActionResult<SolicitudProveedorDTO>> ObtenerSolicitud(int idSolicitud)
        //{
        //    try
        //    {
        //        var solicitud = SolicitudProveedorMapper.ToDTO(await this.gestionarSolicitudProveedorBW.obtenerSolicitud(idSolicitud));

        //        solicitud.Requerimientos = RequerimientoProveedorMapper
        //            .ToDTO(await this.gestionarRequerimientoProveedorBW
        //            .ConsultarRequerimientosProveedor(solicitud.IdSolicitudProveedor),
        //            solicitud.IdSolicitudProveedor);


        //        if (solicitud == null)
        //        {
        //            return NotFound(new { Message = "Solicitud no encontrada" });
        //        }

        //        return Ok(solicitud);
        //    }
        //    catch (Exception ex)
        //    {
        //        Registro de error si es necesario
        //        return StatusCode(500, new { Message = "Ocurrió un error al obtener la solicitud", Details = ex.Message });
        //    }
        //}

        [HttpPut]
        [Route("aprobarSolicitudProveedor")]
        public async Task<IActionResult> AprobarSolicitudProveedor(int idSolicitudProveedor, int idUsuario, string? observacion)
        {
            try
            {
                bool resultado = await this.gestionarSolicitudProveedorBW.AprobarSolicitudProveedor(idSolicitudProveedor, idUsuario, observacion);

                if (resultado)
                {
                    return Ok(new { mensaje = "Estado actualizado a Aprobado correctamente." });
                }
                else
                {
                    return BadRequest(new { mensaje = "No se pudo actualizar el estado de la solicitud." });
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return StatusCode(500, new { mensaje = $"Ocurrió un error al actualizar el estado a Aprobado: {ex.Message}" });
            }
        }

    }
}
