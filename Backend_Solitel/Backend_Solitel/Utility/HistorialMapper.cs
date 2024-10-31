using Backend_Solitel.DTO;
using BC.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Solitel.Utility
{
    public static class HistorialMapper
    {
        public static Historial ToModel(this HistorialDTO historialDTO)
        {
            if (historialDTO == null) return null;

            return new Historial
            {
                IdHistorial = historialDTO.IdHistorial,
                Observacion = historialDTO.Observacion,
                FechaEstado = historialDTO.FechaEstado,
                estado = EstadoMapper.ToModel(historialDTO.estadoDTO),
                IdAnalisis = historialDTO.IdAnalisis,
                usuario = UsuarioMapper.ToModel(historialDTO.usuarioDTO),
                IdSolicitudProveedor = historialDTO.IdSolicitudProveedor
            };
        }

        public static List<Historial> ToModel(this List<HistorialDTO> historialesDTO)
        {
            if (historialesDTO == null)
                return null;

            return historialesDTO.Select(c => c.ToModel()).ToList();
        }

        public static HistorialDTO ToDTO(this Historial historial)
        {
            if (historial == null) return null;

            return new HistorialDTO
            {
                IdHistorial = historial.IdHistorial,
                Observacion = historial.Observacion,
                FechaEstado = historial.FechaEstado,
                usuarioDTO = UsuarioMapper.ToDTO(historial.usuario),
                IdAnalisis = historial.IdAnalisis,
                estadoDTO = EstadoMapper.ToDTO(historial.estado),
                IdSolicitudProveedor = historial.IdSolicitudProveedor
            };
        }

        public static List<HistorialDTO> ToDTO(this List<Historial> historiales)
        {
            if (historiales == null)
                return null;

            return historiales.Select(c => c.ToDTO()).ToList();
        }

    }
}
