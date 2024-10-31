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
                IdOficina = oficinaDTO.IdOficina,
                Nombre = oficinaDTO.Nombre,
                Tipo = oficinaDTO.Tipo
            };
        }

        public static OficinaDTO ToDTO(this Oficina oficina)
        {
            if (oficina == null)
                return null;

            return new OficinaDTO
            {
                IdOficina = oficina.IdOficina,
                Nombre = oficina.Nombre,
                Tipo = oficina.Tipo
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
