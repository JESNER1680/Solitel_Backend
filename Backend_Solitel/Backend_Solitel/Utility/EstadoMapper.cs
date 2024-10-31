using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class EstadoMapper
    {
        public static Estado ToModel(EstadoDTO estadoDTO)
        {
            return new Estado
            {
                IdEstado = estadoDTO.TN_IdEstado,
                Nombre = estadoDTO.TC_Nombre,
                Descripcion = estadoDTO.TC_Descripcion,
                Tipo = estadoDTO.TC_Tipo,
            };
        }

        public static EstadoDTO ToDTO(this Estado estado)  // Método de extensión para Estado
        {
            return new EstadoDTO
            {
                TN_IdEstado = estado.IdEstado,
                TC_Nombre = estado.Nombre,
                TC_Descripcion = estado.Descripcion,
                TC_Tipo = estado.Tipo
            };
        }

        public static List<EstadoDTO> ToDTO(this List<Estado> estados)
        {
            if (estados == null)
                return null;

            return estados.Select(c => c.ToDTO()).ToList();
        }
    }
}
