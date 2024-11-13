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
                IdObjetivoAnalisis = objetivoAnalisisDTO.IdObjetivoAnalisis,
                Nombre = objetivoAnalisisDTO.Nombre,
                Descripcion = objetivoAnalisisDTO.Descripcion
            };
        }

        public static List<ObjetivoAnalisisDTO> ToDTOS(this List<ObjetivoAnalisis>objetivoAnalisis)
        {
            var objetivosRecuperados = objetivoAnalisis.Select(obj => new ObjetivoAnalisisDTO
            {
                IdObjetivoAnalisis = obj.IdObjetivoAnalisis,
                Nombre = obj.Nombre,
                Descripcion = obj.Descripcion
            }).ToList();
            return objetivosRecuperados;
        }

        public static ObjetivoAnalisisDTO ToDTO(this ObjetivoAnalisis objetivoAnalisis)
        {
            var objetivosRecuperados =  new ObjetivoAnalisisDTO
            {
                IdObjetivoAnalisis = objetivoAnalisis.IdObjetivoAnalisis,
                Nombre = objetivoAnalisis.Nombre,
                Descripcion = objetivoAnalisis.Descripcion
            };
            return objetivosRecuperados;
        }
    }
}
