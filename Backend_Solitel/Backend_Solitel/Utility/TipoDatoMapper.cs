using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class TipoDatoMapper
    {
        public static TipoDato ToModel(this TipoDatoDTO tipoDatoDTO)
        {
            return new TipoDato
            {
                IdTipoDato = tipoDatoDTO.IdTipoDato,
                Nombre = tipoDatoDTO.Nombre,
                Descripcion = tipoDatoDTO .Descripcion,
            };
        }

        public static TipoDatoDTO ToDTO(this TipoDato tipoDato)
        {
            if (tipoDato == null)
                return null;

            return new TipoDatoDTO
            {
                IdTipoDato = tipoDato.IdTipoDato,
                Nombre = tipoDato.Nombre,
                Descripcion = tipoDato.Descripcion
            };
        }

        public static List<TipoDatoDTO> ToDTO(this List<TipoDato> tiposDato)
        {
            if (tiposDato == null)
                return null;

            return tiposDato.Select(c => c.ToDTO()).ToList();
        }
    }
}
