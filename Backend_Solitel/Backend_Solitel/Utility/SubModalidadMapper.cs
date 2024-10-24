using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class SubModalidadMapper
    {
        public static SubModalidad ToModel(this SubModalidadDTO subModalidadDTO)
        {
            return new SubModalidad
            {
                TN_IdModalida = subModalidadDTO.IdModalida,
                TC_Nombre = subModalidadDTO.Nombre,
                TC_Descripcion = subModalidadDTO.Descripcion,
                TN_IdSubModalidad = subModalidadDTO.IdSubModalidad
            };
        }

        public static SubModalidadDTO ToDTO(this SubModalidad subModalidad)
        {
            return new SubModalidadDTO
            {
                IdModalida = subModalidad.TN_IdModalida,
                Nombre = subModalidad.TC_Nombre,
                Descripcion = subModalidad.TC_Descripcion,
                IdSubModalidad = subModalidad.TN_IdSubModalidad
            };
        }

        public static List<SubModalidadDTO> ToDTO(this List<SubModalidad> subModalidades)
        {
            if (subModalidades == null)
                return null;

            return subModalidades.Select(c => c.ToDTO()).ToList();
        }
    }
}
