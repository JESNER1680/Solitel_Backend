using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Entidades
{
    [Table("TSOLITEL_SolicitudAnalisis")]
    public class TSOLITEL_SolicitudAnalisisDA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TN_IdAnalisis { get; set; }

        [Required]
        public DateTime TF_FechaDeHecho { get; set; }

        [Required]
        public int TN_IdUsuario { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_OtrosDetalles { get; set; }

        [StringLength(255)]
        public string? TC_OtrosObjetivosDeAnalisis { get; set; }

        [Required]
        public bool TB_Aprobado { get; set; }
        [Required]
        public int TN_IdEstado { get; set; }
        [Required]
        public string TC_NombreEstado { get; set; }

        public DateTime? TF_FechaDeCreacion { get; set; }

        public string TC_NombreUsuario { get;set; }

        public string TC_ApellidoUsuario { get; set; }

        [Required]
        public int TN_IdOficinaSolicitante { get; set; }

    }

}
