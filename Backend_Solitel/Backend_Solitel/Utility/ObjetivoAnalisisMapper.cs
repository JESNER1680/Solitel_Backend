using Backend_Solitel.DTO;
using BC.Modelos;
using System.Runtime.CompilerServices;

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

        public static List<ObjetivoAnalisisDTO> ToDTOS(List<ObjetivoAnalisis>objetivoAnalisis)
        {
            var objetivosRecuperados = objetivoAnalisis.Select(obj => new ObjetivoAnalisisDTO
            {
                TN_IdObjetivoAnalisis = obj.TN_IdObjetivoAnalisis,
                TC_Nombre = obj.TC_Nombre,
                TC_Descripcion = obj.TC_Descripcion
            }).ToList();
            return objetivosRecuperados;
        }
    }
}
