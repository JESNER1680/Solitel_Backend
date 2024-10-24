using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class FiscaliaMapper
    {
        public static Fiscalia ToModel(this FiscaliaDTO fiscaliaDTO)
        {
            return new Fiscalia
            {
                TN_IdFiscalia = fiscaliaDTO.IdFiscalia,
                TC_Nombre = fiscaliaDTO.Nombre
            };
        }

        public static FiscaliaDTO ToDTO(this Fiscalia fiscalia)
        {
            return new FiscaliaDTO
            {
                IdFiscalia = fiscalia.TN_IdFiscalia,
                Nombre = fiscalia.TC_Nombre
            };
        }
        public static List<FiscaliaDTO> ToDTO(this List<Fiscalia> fiscalias)
        {
            if (fiscalias == null)
                return null;

            return fiscalias.Select(c => c.ToDTO()).ToList();
        }

        public static List<Fiscalia> ToModel(this List<FiscaliaDTO> fiscaliasDTO)
        {
            if (fiscaliasDTO == null)
                return null;

            return fiscaliasDTO.Select(c => c.ToModel()).ToList();
        }
    }
}
