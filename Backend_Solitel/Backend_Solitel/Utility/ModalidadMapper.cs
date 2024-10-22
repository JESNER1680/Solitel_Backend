using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class ModalidadMapper
    {
        public static Modalidad ToModel(ModalidadDTO modalidadDTO)
        {
            return new Modalidad
            {
                TN_IdModalidad = modalidadDTO.TN_IdModalidad,
                TC_Nombre = modalidadDTO.TC_Nombre,
                TC_Descripcion = modalidadDTO.TC_Descripcion
            };
        }

        public static ModalidadDTO ToDTO(Modalidad modalidad)
        {
            return new ModalidadDTO
            {
                TN_IdModalidad = modalidad.TN_IdModalidad,
                TC_Nombre = modalidad.TC_Nombre,
                TC_Descripcion = modalidad.TC_Descripcion
            };
        }
    }
}
