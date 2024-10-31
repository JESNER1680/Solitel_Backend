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
                IdObjetivoAnalisis = objetivoAnalisisDTO.TN_IdObjetivoAnalisis,
                Nombre = objetivoAnalisisDTO.TC_Nombre,
                Descripcion = objetivoAnalisisDTO.TC_Descripcion
            };
        }

        public static List<ObjetivoAnalisisDTO> ToDTOS(this List<ObjetivoAnalisis>objetivoAnalisis)
        {
            var objetivosRecuperados = objetivoAnalisis.Select(obj => new ObjetivoAnalisisDTO
            {
                TN_IdObjetivoAnalisis = obj.IdObjetivoAnalisis,
                TC_Nombre = obj.Nombre,
                TC_Descripcion = obj.Descripcion
            }).ToList();
            return objetivosRecuperados;
        }

        public static ObjetivoAnalisisDTO ToDTO(this ObjetivoAnalisis objetivoAnalisis)
        {
            var objetivosRecuperados =  new ObjetivoAnalisisDTO
            {
                TN_IdObjetivoAnalisis = objetivoAnalisis.IdObjetivoAnalisis,
                TC_Nombre = objetivoAnalisis.Nombre,
                TC_Descripcion = objetivoAnalisis.Descripcion
            };
            return objetivosRecuperados;
        }
    }
}
