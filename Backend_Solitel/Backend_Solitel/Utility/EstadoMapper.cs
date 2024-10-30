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
                TN_IdEstado = estadoDTO.TN_IdEstado,
                TC_Nombre = estadoDTO.TC_Nombre,
                TC_Descripcion = estadoDTO.TC_Descripcion,
                TC_Tipo = estadoDTO.TC_Tipo,
            };
        }

        public static EstadoDTO ToDTO(this Estado estado)  // Método de extensión para Estado
        {
            return new EstadoDTO
            {
                TN_IdEstado = estado.TN_IdEstado,
                TC_Nombre = estado.TC_Nombre,
                TC_Descripcion = estado.TC_Descripcion,
                TC_Tipo = estado.TC_Tipo
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
