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
                TC_Descripcion = estadoDTO.TC_Descripcion
            };
        }

        public static EstadoDTO ToDTO(Estado estado)
        {
            return new EstadoDTO
            {
                TN_IdEstado = estado.TN_IdEstado,
                TC_Nombre = estado.TC_Nombre,
                TC_Descripcion = estado.TC_Descripcion
            };
        }
    }
}
