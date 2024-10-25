using Backend_Solitel.DTO;
using BC.Modelos;
using BC.Reglas_de_Negocio;
using System.Collections.Generic;

namespace Backend_Solitel.Utility
{
    public static class TipoSolicitudMapper
    {
        public static TipoSolicitud ToModel(TipoSolicitudDTO tipoSolicitudDTO)
        {
            var tipoSolicitud = new TipoSolicitud
            {
                TN_IdTipoSolicitud = tipoSolicitudDTO.IdTipoSolicitud,
                TC_Nombre = tipoSolicitudDTO.Nombre,
                TC_Descripcion = tipoSolicitudDTO.Descripcion
            };

            return tipoSolicitud;
        }

        public static List<TipoSolicitud> ToModel(List<TipoSolicitudDTO> tipoSolicitudesDTO)
        {
            List<TipoSolicitud> tipoSolicitudes = new List<TipoSolicitud>(); 
            foreach (TipoSolicitudDTO tipoSolicitudDTO in tipoSolicitudesDTO)
            {
                tipoSolicitudes.Add(new TipoSolicitud
                {
                    TN_IdTipoSolicitud = tipoSolicitudDTO.IdTipoSolicitud,
                    TC_Nombre = tipoSolicitudDTO.Nombre,
                    TC_Descripcion = tipoSolicitudDTO.Descripcion
                });
            }

            return tipoSolicitudes;
        }

        public static TipoSolicitudDTO ToDTO(TipoSolicitud tipoSolicitud)
        {
            var tipoSolicitudDTO = new TipoSolicitudDTO
            {
                IdTipoSolicitud = tipoSolicitud.TN_IdTipoSolicitud,
                Nombre = tipoSolicitud.TC_Nombre,
                Descripcion = tipoSolicitud.TC_Descripcion
            };

            return tipoSolicitudDTO;
        }

        public static List<TipoSolicitudDTO> ToDTO(List<TipoSolicitud> tipoSolicitudesDTO)
        {
            List<TipoSolicitudDTO> tipoSolicitudes = new List<TipoSolicitudDTO>();
            foreach (TipoSolicitud tipoSolicitudDTO in tipoSolicitudesDTO)
            {
                tipoSolicitudes.Add(new TipoSolicitudDTO
                {
                    IdTipoSolicitud = tipoSolicitudDTO.TN_IdTipoSolicitud,
                    Nombre = tipoSolicitudDTO.TC_Nombre,
                    Descripcion = tipoSolicitudDTO.TC_Descripcion
                });
            }

            return tipoSolicitudes;
        }
    }
}
