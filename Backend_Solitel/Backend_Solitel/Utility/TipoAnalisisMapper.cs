using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class TipoAnalisisMapper
    {
        public static TipoAnalisis ToModel(this TipoAnalisisDTO tipoAnalisisDTO)
        {
            return new TipoAnalisis
            {
                IdTipoAnalisis = tipoAnalisisDTO.IdTipoAnalisis,
                Nombre = tipoAnalisisDTO.Nombre,
                Descripcion = tipoAnalisisDTO.Descripcion
            };
        }

        public static TipoAnalisisDTO ToDTO(this TipoAnalisis tipoAnalisis)
        {
            return new TipoAnalisisDTO
            {
                IdTipoAnalisis = tipoAnalisis.IdTipoAnalisis,
                Nombre = tipoAnalisis.Nombre,
                Descripcion = tipoAnalisis.Descripcion
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
