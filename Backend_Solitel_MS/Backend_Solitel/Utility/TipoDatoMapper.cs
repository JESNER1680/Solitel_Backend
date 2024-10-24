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
                TN_IdTipoDato = tipoDatoDTO.IdTipoDato,
                TC_Nombre = tipoDatoDTO.Nombre,
                TC_Descripcion = tipoDatoDTO .Descripcion,
            };
        }

        public static TipoDatoDTO ToDTO(this TipoDato tipoDato)
        {
            if (tipoDato == null)
                return null;

            return new TipoDatoDTO
            {
                IdTipoDato = tipoDato.TN_IdTipoDato,
                Nombre = tipoDato.TC_Nombre,
                Descripcion = tipoDato.TC_Descripcion
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
