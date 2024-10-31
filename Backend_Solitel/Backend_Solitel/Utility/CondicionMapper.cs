using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class CondicionMapper
    {
        // Mapeo de CategoriaDelito a CategoriaDelitoDTO
        public static CondicionDTO ToDTO(this Condicion condicion)
        {
            if (condicion == null)
                return null;

            return new CondicionDTO
            {
                IdCondicion = condicion.IdCondicion,
                Nombre = condicion.Nombre,
                Descripcion = condicion.Descripcion
            };
        }

        // Mapeo de lista de CategoriaDelito a CategoriaDelitoDTO
        public static List<CondicionDTO> ToDTO(this List<Condicion> condiciones)
        {
            if (condiciones == null)
                return null;

            return condiciones.Select(c => c.ToDTO()).ToList();
        }

        // Mapeo de CategoriaDelitoDTO a CategoriaDelito
        public static Condicion ToModel(this CondicionDTO condicionDTO)
        {
            if (condicionDTO == null)
                return null;

            return new Condicion
            {
                IdCondicion = condicionDTO.IdCondicion,
                Nombre = condicionDTO.Nombre,
                Descripcion = condicionDTO.Descripcion
            };
        }

        // Mapeo de lista de CategoriaDelitoDTO a CategoriaDelito
        public static List<Condicion> ToModel(this List<CondicionDTO> condicionesDTO)
        {
            if (condicionesDTO == null)
                return null;

            return condicionesDTO.Select(c => c.ToModel()).ToList();
        }
    }
}
