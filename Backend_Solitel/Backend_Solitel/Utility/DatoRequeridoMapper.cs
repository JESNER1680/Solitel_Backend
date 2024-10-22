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

        public static List<DatoRequeridoDTO> ToDTO(List<DatoRequerido> datosRequeridos)
        {
            List<DatoRequeridoDTO> datosRequeridosDTO = new List<DatoRequeridoDTO>();
            foreach (DatoRequerido datoRequerido in datosRequeridos)
            {
                datosRequeridosDTO.Add(new DatoRequeridoDTO
                {
                    TN_IdDatoRequerido = datoRequerido.TN_IdDatoRequerido,
                    TC_DatoRequerido = datoRequerido.TC_DatoRequerido,
                    TC_Motivacion = datoRequerido.TC_Motivacion,
                    TN_IdTipoDato = datoRequerido.TN_IdTipoDato
                });
            }

            return datosRequeridosDTO;
        }
    }
}
