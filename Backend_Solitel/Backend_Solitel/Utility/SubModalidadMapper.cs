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
                IdModalidad = subModalidadDTO.IdModalida,
                Nombre = subModalidadDTO.Nombre,
                Descripcion = subModalidadDTO.Descripcion,
                IdSubModalidad = subModalidadDTO.IdSubModalidad
            };
        }

        public static SubModalidadDTO ToDTO(this SubModalidad subModalidad)
        {
            return new SubModalidadDTO
            {
                IdModalida = subModalidad.IdModalidad,
                Nombre = subModalidad.Nombre,
                Descripcion = subModalidad.Descripcion,
                IdSubModalidad = subModalidad.IdSubModalidad
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
