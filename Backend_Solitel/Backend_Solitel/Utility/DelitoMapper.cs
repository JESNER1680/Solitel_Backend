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
                IdDelito = delitoDTO.IdDelito,
                Nombre = delitoDTO.Nombre,
                Descripcion = delitoDTO.Descripcion,
                IdCategoriaDelito = delitoDTO.IdCategoriaDelito,
            };
        }

        public static DelitoDTO ToDTO(this Delito delito)
        {
            return new DelitoDTO
            {
                IdDelito = delito.IdDelito,
                Nombre = delito.Nombre,
                Descripcion = delito.Descripcion,
                IdCategoriaDelito = delito.IdCategoriaDelito
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
