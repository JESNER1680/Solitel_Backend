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
                IdHistorial = historialDTO.TN_IdHistorial,
                Observacion = historialDTO.TC_Observacion,
                FechaEstado = historialDTO.TF_FechaEstado,
                estado = EstadoMapper.ToModel(historialDTO.estadoDTO),
                IdAnalisis = historialDTO.TN_IdAnalisis,
                usuario = UsuarioMapper.ToModel(historialDTO.usuarioDTO),
                IdSolicitudProveedor = historialDTO.TN_IdSolicitudProveedor
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
                TN_IdHistorial = historial.IdHistorial,
                TC_Observacion = historial.Observacion,
                TF_FechaEstado = historial.FechaEstado,
                usuarioDTO = UsuarioMapper.ToDTO(historial.usuario),
                TN_IdAnalisis = historial.IdAnalisis,
                estadoDTO = EstadoMapper.ToDTO(historial.estado),
                TN_IdSolicitudProveedor = historial.IdSolicitudProveedor
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
