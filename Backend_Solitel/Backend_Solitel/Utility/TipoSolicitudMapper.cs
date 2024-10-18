using Backend_Solitel.DTO;
using BC.Modelos;
using System.Collections.Generic;

namespace Backend_Solitel.Utility
{
    public static class TipoSolicitudMapper
    {
        public static List<TipoSolicitud> ToModel(List<TipoSolicitudDTO> tipoSolicitudesDTO)
        {
            List<TipoSolicitud> tipoSolicitudes = new List<TipoSolicitud>(); 
            foreach (TipoSolicitudDTO tipoSolicitudDTO in tipoSolicitudesDTO)
            {
                tipoSolicitudes.Add(new TipoSolicitud
                {
                    TN_IdTipoSolicitud = tipoSolicitudDTO.TN_IdTipoSolicitud,
                    TC_Nombre = tipoSolicitudDTO.TC_Nombre,
                    TC_Descripcion = tipoSolicitudDTO.TC_Descripcion
                });
            }

            return tipoSolicitudes;
        }
    }
}
