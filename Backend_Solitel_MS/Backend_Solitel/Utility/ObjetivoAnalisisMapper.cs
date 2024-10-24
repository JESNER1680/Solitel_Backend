using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class ObjetivoAnalisisMapper
    {
        public static ObjetivoAnalisis ToModel(ObjetivoAnalisisDTO objetivoAnalisisDTO)
        {
            return new ObjetivoAnalisis
            {
                TN_IdObjetivoAnalisis = objetivoAnalisisDTO.TN_IdObjetivoAnalisis,
                TC_Nombre = objetivoAnalisisDTO.TC_Nombre,
                TC_Descripcion = objetivoAnalisisDTO.TC_Descripcion
            };
        }
    }
}
