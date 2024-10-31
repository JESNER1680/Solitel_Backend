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
                IdOficina = oficinaDTO.TN_IdOficina,
                Nombre = oficinaDTO.TC_Nombre,
                Tipo = oficinaDTO.TC_Tipo
            };
        }

        public static OficinaDTO ToDTO(this Oficina oficina)
        {
            if (oficina == null)
                return null;

            return new OficinaDTO
            {
                TN_IdOficina = oficina.IdOficina,
                TC_Nombre = oficina.Nombre,
                TC_Tipo = oficina.Tipo
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
