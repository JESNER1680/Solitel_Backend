﻿using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class TipoAnalisisMapper
    {
        public static TipoAnalisis ToModel(this TipoAnalisisDTO tipoAnalisisDTO)
        {
            return new TipoAnalisis
            {
                TN_IdTipoAnalisis = tipoAnalisisDTO.IdTipoAnalisis,
                TC_Nombre = tipoAnalisisDTO.Nombre,
                TC_Descripcion = tipoAnalisisDTO.Descripcion
            };
        }

        public static TipoAnalisisDTO ToDTO(this TipoAnalisis tipoAnalisis)
        {
            return new TipoAnalisisDTO
            {
                IdTipoAnalisis = tipoAnalisis.TN_IdTipoAnalisis,
                Nombre = tipoAnalisis.TC_Nombre,
                Descripcion = tipoAnalisis.TC_Descripcion
            };
        }

        public static List<TipoAnalisisDTO> ToDTO(this List<TipoAnalisis> tiposAnalisis)
        {
            if (tiposAnalisis == null)
                return null;

            return tiposAnalisis.Select(c => c.ToDTO()).ToList();
        }
    }
}
