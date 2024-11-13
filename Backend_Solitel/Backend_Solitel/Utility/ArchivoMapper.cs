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
                IdArchivo = archivoDTO.IdArchivo,
                Nombre = archivoDTO.Nombre,
                FormatoArchivo = archivoDTO.FormatoAchivo,
                Contenido = archivoDTO.Contenido,
                FechaModificacion = archivoDTO.FechaModificacion
            };
        }
    }
}
