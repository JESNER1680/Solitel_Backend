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
                IdEstado = estadoDTO.IdEstado,
                Nombre = estadoDTO.Nombre,
                Descripcion = estadoDTO.Descripcion,
                Tipo = estadoDTO.Tipo,
                CantidadSolicitudes = estadoDTO.CantidadSolicitudes
            };
        }

        public static EstadoDTO ToDTO(this Estado estado)  // Método de extensión para Estado
        {
            return new EstadoDTO
            {
                IdEstado = estado.IdEstado,
                Nombre = estado.Nombre,
                Descripcion = estado.Descripcion,
                Tipo = estado.Tipo,
                CantidadSolicitudes = estado.CantidadSolicitudes
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
