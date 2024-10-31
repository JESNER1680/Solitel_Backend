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
                    IdDatoRequerido = datoRequeridoDTO.TN_IdDatoRequerido,
                    DatoRequeridoContenido = datoRequeridoDTO.TC_DatoRequerido,
                    Motivacion = datoRequeridoDTO.TC_Motivacion,
                    IdTipoDato = datoRequeridoDTO.TN_IdTipoDato
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
                    TN_IdDatoRequerido = datoRequerido.IdDatoRequerido,
                    TC_DatoRequerido = datoRequerido.DatoRequeridoContenido,
                    TC_Motivacion = datoRequerido.Motivacion,
                    TN_IdTipoDato = datoRequerido.IdTipoDato
                });
            }

            return datosRequeridosDTO;
        }
    }
}
