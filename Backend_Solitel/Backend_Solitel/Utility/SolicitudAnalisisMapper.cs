using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public class SolicitudAnalisisMapper
    {
        public static SolicitudAnalisis ToModel(SolicitudAnalisisDTO solicitudAnalisisDTO)
        {
            if (solicitudAnalisisDTO == null)
                return null;

            DateTime fechaActual = DateTime.Now;

            return new SolicitudAnalisis
            {
                TN_IdSolicitudAnalisis = solicitudAnalisisDTO.TN_IdSolicitudAnalisis,
                TF_FechaDelHecho = solicitudAnalisisDTO.TF_FechaDelHecho,
                TC_OtrosDetalles = solicitudAnalisisDTO.TC_OtrosDetalles,
                TC_OtrosObjetivosDeAnalisis = solicitudAnalisisDTO.TC_OtrosObjetivosDeAnalisis,
                TB_Aprobado = solicitudAnalisisDTO.TB_Aprobado,
                TF_FechaCrecion = solicitudAnalisisDTO.TF_FechaCrecion ?? fechaActual,
                TN_NumeroSolicitud = solicitudAnalisisDTO.TN_NumeroSolicitud,
                TN_IdOficina = solicitudAnalisisDTO.TN_IdOficina,
                requerimentos = new List<RequerimentoAnalisis>() // Aquí puedes mapear la lista si es necesario
            };
        }

        // Convierte de Modelo a DTO
        public static SolicitudAnalisisDTO ToDTO(SolicitudAnalisis solicitudAnalisis)
        {
            if (solicitudAnalisis == null)
                return null;

            return new SolicitudAnalisisDTO
            {
                TN_IdSolicitudAnalisis = solicitudAnalisis.TN_IdSolicitudAnalisis,
                TF_FechaDelHecho = solicitudAnalisis.TF_FechaDelHecho,
                TC_OtrosDetalles = solicitudAnalisis.TC_OtrosDetalles,
                TC_OtrosObjetivosDeAnalisis = solicitudAnalisis.TC_OtrosObjetivosDeAnalisis,
                TB_Aprobado = solicitudAnalisis.TB_Aprobado,
                TF_FechaCrecion = solicitudAnalisis.TF_FechaCrecion,
                TN_NumeroSolicitud = solicitudAnalisis.TN_NumeroSolicitud,
                TN_IdOficina = solicitudAnalisis.TN_IdOficina
            };
        }

        // Convierte una lista de modelos a una lista de DTOs
        public static List<SolicitudAnalisisDTO> ToDTO(List<SolicitudAnalisis> solicitudesAnalisis)
        {
            if (solicitudesAnalisis == null)
                return null;

            return solicitudesAnalisis.Select(c => ToDTO(c)).ToList();
        }

        // Convierte una lista de DTOs a una lista de modelos
        public static List<SolicitudAnalisis> ToModel(List<SolicitudAnalisisDTO> solicitudesAnalisisDTO)
        {
            if (solicitudesAnalisisDTO == null)
                return null;

            return solicitudesAnalisisDTO.Select(c => ToModel(c)).ToList();
        }
    }
}
