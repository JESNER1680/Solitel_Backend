using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class DatoRequeridoMapper
    {
        public static List<DatoRequerido> ToModel(List<DatoRequeridoDTO> datosRequeridosDTO)
        {
            List<DatoRequerido> datosRequeridos = new List<DatoRequerido>();
            foreach (DatoRequeridoDTO datoRequeridoDTO in datosRequeridosDTO)
            {
                datosRequeridos.Add(new DatoRequerido
                {
                    TN_IdDatoRequerido = datoRequeridoDTO.TN_IdDatoRequerido,
                    TC_DatoRequerido = datoRequeridoDTO.TC_DatoRequerido,
                    TC_Motivacion = datoRequeridoDTO.TC_Motivacion,
                    TN_IdTipoDato = datoRequeridoDTO.TN_IdTipoDato
                });
            }

            return datosRequeridos;
        }
    }
}
