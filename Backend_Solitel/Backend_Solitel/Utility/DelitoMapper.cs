using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class DelitoMapper
    {
        public static Delito ToModel(DelitoDTO delitoDTO)
        {
            return new Delito
            {
                TN_IdDelito = delitoDTO.TN_IdDelito,
                TC_Nombre = delitoDTO.TC_Nombre,
                TC_Descripcion = delitoDTO.TC_Descripcion,
                TN_IdCategoriaDelito = delitoDTO.TN_IdCategoriaDelito
            };
        }

        public static DelitoDTO ToDTO(Delito delito)
        {
            return new DelitoDTO
            {
                TN_IdDelito = delito.TN_IdDelito,
                TC_Nombre = delito.TC_Nombre,
                TC_Descripcion = delito.TC_Descripcion,
                TN_IdCategoriaDelito = delito.TN_IdCategoriaDelito
            };
        }
    }
}
