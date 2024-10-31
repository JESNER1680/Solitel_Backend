using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class ArchivoMapper
    {
        public static Archivo ToModel(ArchivoDTO archivoDTO)
        {
            return new Archivo
            {
                IdArchivo = archivoDTO.TN_IdArchivo,
                Nombre = archivoDTO.TC_Nombre,
                FormatoArchivo = archivoDTO.TC_FormatoAchivo,
                Contenido = archivoDTO.TV_Contenido,
                FechaModificacion = archivoDTO.TF_FechaModificacion
            };
        }
    }
}
