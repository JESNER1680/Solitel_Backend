using System.ComponentModel.DataAnnotations;

namespace Backend_Solitel.DTO
{
    public class ArchivoDTO
    {
        public int IdArchivo { get; set; }


        [Required]
        public string Nombre { get; set; }

        [Required]
        public byte[] Contenido { get; set; }

        [Required]
        [StringLength(50)]
        public string FormatoAchivo { get; set; }

        [Required]
        public DateTime FechaModificacion { get; set; }
    }

}
