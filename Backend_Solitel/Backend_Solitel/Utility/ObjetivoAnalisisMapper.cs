using Backend_Solitel.DTO;
using BC.Modelos;
using System.Runtime.CompilerServices;

namespace Backend_Solitel.Utility
{
    public static class ObjetivoAnalisisMapper
    {
        public static ObjetivoAnalisis ToModel(this ObjetivoAnalisisDTO objetivoAnalisisDTO)
        {
            return new ObjetivoAnalisis
            {
                TN_IdObjetivoAnalisis = objetivoAnalisisDTO.TN_IdObjetivoAnalisis,
                TC_Nombre = objetivoAnalisisDTO.TC_Nombre,
                TC_Descripcion = objetivoAnalisisDTO.TC_Descripcion
            };
        }

        public static List<ObjetivoAnalisisDTO> ToDTOS(this List<ObjetivoAnalisis>objetivoAnalisis)
        {
            var objetivosRecuperados = objetivoAnalisis.Select(obj => new ObjetivoAnalisisDTO
            {
                TN_IdObjetivoAnalisis = obj.TN_IdObjetivoAnalisis,
                TC_Nombre = obj.TC_Nombre,
                TC_Descripcion = obj.TC_Descripcion
            }).ToList();
            return objetivosRecuperados;
        }

        public static ObjetivoAnalisisDTO ToDTO(this ObjetivoAnalisis objetivoAnalisis)
        {
            var objetivosRecuperados =  new ObjetivoAnalisisDTO
            {
                TN_IdObjetivoAnalisis = objetivoAnalisis.TN_IdObjetivoAnalisis,
                TC_Nombre = objetivoAnalisis.TC_Nombre,
                TC_Descripcion = objetivoAnalisis.TC_Descripcion
            };
            return objetivosRecuperados;
        }
    }
}
