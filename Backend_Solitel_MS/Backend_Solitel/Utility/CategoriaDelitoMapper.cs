using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class CategoriaDelitoMapper
    {
        // Mapeo de CategoriaDelito a CategoriaDelitoDTO
        public static CategoriaDelitoDTO ToDTO(this CategoriaDelito categoriaDelito)
        {
            if (categoriaDelito == null)
                return null;

            return new CategoriaDelitoDTO
            {
                IdCategoriaDelito = categoriaDelito.TN_IdCategoriaDelito,
                Nombre = categoriaDelito.TC_Nombre,
                Descripcion = categoriaDelito.TC_Descripcion
            };
        }

        // Mapeo de lista de CategoriaDelito a CategoriaDelitoDTO
        public static List<CategoriaDelitoDTO> ToDTO(this List<CategoriaDelito> categoriasDelito)
        {
            if (categoriasDelito == null)
                return null;

            return categoriasDelito.Select(c => c.ToDTO()).ToList();
        }

        // Mapeo de CategoriaDelitoDTO a CategoriaDelito
        public static CategoriaDelito ToModel(this CategoriaDelitoDTO categoriaDelitoDTO)
        {
            if (categoriaDelitoDTO == null)
                return null;

            return new CategoriaDelito
            {
                TN_IdCategoriaDelito = categoriaDelitoDTO.IdCategoriaDelito,
                TC_Nombre = categoriaDelitoDTO.Nombre,
                TC_Descripcion = categoriaDelitoDTO.Descripcion
            };
        }

        // Mapeo de lista de CategoriaDelitoDTO a CategoriaDelito
        public static List<CategoriaDelito> ToModel(this List<CategoriaDelitoDTO> categoriasDelitoDTO)
        {
            if (categoriasDelitoDTO == null)
                return null;

            return categoriasDelitoDTO.Select(c => c.ToModel()).ToList();
        }
    }
}
