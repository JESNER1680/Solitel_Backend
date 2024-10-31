using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class ModalidadMapper
    {
        public static Modalidad ToModel(this ModalidadDTO modalidadDTO)
        {
            return new Modalidad
            {
                IdModalidad = modalidadDTO.IdModalidad,
                Nombre = modalidadDTO.Nombre,
                Descripcion = modalidadDTO.Descripcion
            };
        }

        public static ModalidadDTO ToDTO(this Modalidad modalidad)
        {
            return new ModalidadDTO
            {
                IdModalidad = modalidad.IdModalidad,
                Nombre = modalidad.Nombre,
                Descripcion = modalidad.Descripcion
            };
        }

        public static List<ModalidadDTO> ToDTO(this List<Modalidad> modalidad)
        {
            if (modalidad == null)
                return null;

            return modalidad.Select(c => c.ToDTO()).ToList();
        }

        public static List<Modalidad> ToModel(this List<ModalidadDTO> modalidadDTO)
        {
            if (modalidadDTO == null)
                return null;

            return modalidadDTO.Select(c => c.ToModel()).ToList();
        }
    }
}
