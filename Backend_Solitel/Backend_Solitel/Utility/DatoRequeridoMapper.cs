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
                    IdDatoRequerido = datoRequeridoDTO.IdDatoRequerido,
                    DatoRequeridoContenido = datoRequeridoDTO.DatoRequeridoContenido,
                    Motivacion = datoRequeridoDTO.Motivacion,
                    IdTipoDato = datoRequeridoDTO.IdTipoDato
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
                    IdDatoRequerido = datoRequerido.IdDatoRequerido,
                    DatoRequeridoContenido = datoRequerido.DatoRequeridoContenido,
                    Motivacion = datoRequerido.Motivacion,
                    IdTipoDato = datoRequerido.IdTipoDato
                });
            }

            return datosRequeridosDTO;
        }
    }
}
