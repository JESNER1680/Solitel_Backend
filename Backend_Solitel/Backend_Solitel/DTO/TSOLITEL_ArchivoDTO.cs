using System.ComponentModel.DataAnnotations;

namespace Backend_Solitel.DTO
{
    public class TSOLITEL_ArchivoDTO
    {
        public int TN_IdArchivo { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_HashAchivo { get; set; }

        [Required]
        public int TC_Nombre { get; set; }

        [Required]
        public byte[] TV_Contenido { get; set; }

        [Required]
        [StringLength(50)]
        public string TC_FormatoAchivo { get; set; }

        [Required]
        public DateTime TF_FechaModificacion { get; set; }

        public int? TN_IdRequerimiento { get; set; }
    }

}
