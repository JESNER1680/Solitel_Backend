using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class SubModalidadMapper
    {
        public static SubModalidad ToModel(SubModalidadDTO subModalidadDTO)
        {
            return new SubModalidad
            {
                TN_IdModalida = subModalidadDTO.TN_IdModalida,
                TC_Nombre = subModalidadDTO.TC_Nombre,
                TC_Descripcion = subModalidadDTO.TC_Descripcion,
                TN_IdSubModalidad = subModalidadDTO.TN_IdSubModalidad
            };
        }

        public static SubModalidadDTO ToDTO(SubModalidad subModalidad)
        {
            return new SubModalidadDTO
            {
                TN_IdModalida = subModalidad.TN_IdModalida,
                TC_Nombre = subModalidad.TC_Nombre,
                TC_Descripcion = subModalidad.TC_Descripcion,
                TN_IdSubModalidad = subModalidad.TN_IdSubModalidad
            };
        }
    }
}
