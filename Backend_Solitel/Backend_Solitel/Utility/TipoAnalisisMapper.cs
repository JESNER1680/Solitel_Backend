using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class TipoAnalisisMapper
    {
        public static TipoAnalisis ToModel(TipoAnalisisDTO tipoAnalisisDTO)
        {
            return new TipoAnalisis
            {
                TN_IdTipoAnalisis = tipoAnalisisDTO.TN_IdTipoAnalisis,
                TC_Nombre = tipoAnalisisDTO.TC_Nombre,
                TC_Descripcion = tipoAnalisisDTO.TC_Descripcion
            };
        }
    }
}
