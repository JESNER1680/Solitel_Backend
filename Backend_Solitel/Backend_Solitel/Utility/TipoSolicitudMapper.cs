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
                IdTipoSolicitud = tipoSolicitudDTO.IdTipoSolicitud,
                Nombre = tipoSolicitudDTO.Nombre,
                Descripcion = tipoSolicitudDTO.Descripcion
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
                    IdTipoSolicitud = tipoSolicitudDTO.IdTipoSolicitud,
                    Nombre = tipoSolicitudDTO.Nombre,
                    Descripcion = tipoSolicitudDTO.Descripcion
                });
            }

            return tipoSolicitudes;
        }

        public static TipoSolicitudDTO ToDTO(TipoSolicitud tipoSolicitud)
        {
            var tipoSolicitudDTO = new TipoSolicitudDTO
            {
                IdTipoSolicitud = tipoSolicitud.IdTipoSolicitud,
                Nombre = tipoSolicitud.Nombre,
                Descripcion = tipoSolicitud.Descripcion
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
                    IdTipoSolicitud = tipoSolicitudDTO.IdTipoSolicitud,
                    Nombre = tipoSolicitudDTO.Nombre,
                    Descripcion = tipoSolicitudDTO.Descripcion
                });
            }

            return tipoSolicitudes;
        }
    }
}
