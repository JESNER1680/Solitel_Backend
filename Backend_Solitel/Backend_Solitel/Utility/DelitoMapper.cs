using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class DelitoMapper
    {
        public static Delito ToModel(this DelitoDTO delitoDTO)
        {
            return new Delito
            {
                TN_IdDelito = delitoDTO.IdDelito,
                TC_Nombre = delitoDTO.Nombre,
                TC_Descripcion = delitoDTO.Descripcion,
                TN_IdCategoriaDelito = delitoDTO.IdCategoriaDelito,
            };
        }

        public static DelitoDTO ToDTO(this Delito delito)
        {
            return new DelitoDTO
            {
                IdDelito = delito.TN_IdDelito,
                Nombre = delito.TC_Nombre,
                Descripcion = delito.TC_Descripcion,
                IdCategoriaDelito = delito.TN_IdCategoriaDelito
            };
        }

        public static List<DelitoDTO> ToDTO(this List<Delito> delitos)
        {
            if (delitos == null)
                return null;

            return delitos.Select(c => c.ToDTO()).ToList();
        }

        public static List<Condicion> ToModel(this List<CondicionDTO> condicionesDTO)
        {
            if (condicionesDTO == null)
                return null;

            return condicionesDTO.Select(c => c.ToModel()).ToList();
        }
    }
}
