using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class TipoDatoMapper
    {
        public static TipoDato ToModel(TipoDatoDTO tipoDatoDTO)
        {
            return new TipoDato
            {
                TN_IdTipoDato = tipoDatoDTO.TN_IdTipoDato,
                TC_Nombre = tipoDatoDTO.TC_Nombre,
                TC_Descripcion = tipoDatoDTO .TC_Descripcion,
            };
        }
    }
}
