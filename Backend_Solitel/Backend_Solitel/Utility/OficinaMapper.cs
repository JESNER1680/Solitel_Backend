using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class OficinaMapper
    {
        public static Oficina ToModel(OficinaDTO oficinaDTO)
        {
            return new Oficina
            {
                TN_IdOficina = oficinaDTO.TN_IdOficina,
                TC_Nombre = oficinaDTO.TC_Nombre
            };
        }

        public static OficinaDTO ToDTO(this Oficina oficina)
        {
            if (oficina == null)
                return null;

            return new OficinaDTO
            {
                TN_IdOficina = oficina.TN_IdOficina,
                TC_Nombre = oficina.TC_Nombre
            };
        }

        public static List<OficinaDTO> ToDTO(this List<Oficina> oficinas)
        {
            if (oficinas == null)
                return null;

            return oficinas.Select(c => c.ToDTO()).ToList();
        }
    }
}
