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
                TN_IdHistorial = historialDTO.TN_IdHistorial,
                TC_Observacion = historialDTO.TC_Observacion,
                TF_FechaEstado = historialDTO.TF_FechaEstado,
                TN_IdEstado = historialDTO.TN_IdEstado,
                TN_IdAnalisis = historialDTO.TN_IdAnalisis,
                TN_IdUsuario = historialDTO.TN_IdUsuario,
                TN_IdSolicitudProveedor = historialDTO.TN_IdSolicitudProveedor
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
                TN_IdHistorial = historial.TN_IdHistorial,
                TC_Observacion = historial.TC_Observacion,
                TF_FechaEstado = historial.TF_FechaEstado,
                TN_IdEstado = historial.TN_IdEstado,
                TN_IdAnalisis = historial.TN_IdAnalisis,
                TN_IdUsuario = historial.TN_IdUsuario,
                TN_IdSolicitudProveedor = historial.TN_IdSolicitudProveedor
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
