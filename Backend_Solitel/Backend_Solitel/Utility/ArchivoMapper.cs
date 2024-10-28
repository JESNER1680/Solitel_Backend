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
                TN_IdArchivo = archivoDTO.TN_IdArchivo,
                TC_Nombre = archivoDTO.TC_Nombre,
                TC_FormatoAchivo = archivoDTO.TC_FormatoAchivo,
                TV_Contenido = archivoDTO.TV_Contenido,
                TF_FechaModificacion = archivoDTO.TF_FechaModificacion,
                TC_HashAchivo = ""
            };
        }
    }
}
