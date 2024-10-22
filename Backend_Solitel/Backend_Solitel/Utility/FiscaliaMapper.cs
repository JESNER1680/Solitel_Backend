using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class FiscaliaMapper
    {
        public static Fiscalia ToModel(FiscaliaDTO fiscaliaDTO)
        {
            return new Fiscalia
            {
                TN_IdFiscalia = fiscaliaDTO.TN_IdFiscalia,
                TC_Nombre = fiscaliaDTO.TC_Nombre
            };
        }

        public static FiscaliaDTO ToDTO(Fiscalia fiscalia)
        {
            return new FiscaliaDTO
            {
                TN_IdFiscalia = fiscalia.TN_IdFiscalia,
                TC_Nombre = fiscalia.TC_Nombre
            };
        }
    }
}
