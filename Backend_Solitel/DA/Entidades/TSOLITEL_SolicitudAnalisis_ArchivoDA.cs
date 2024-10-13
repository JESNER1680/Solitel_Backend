using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_SolicitudAnalisis_Archivo")]
    public class TSOLITEL_SolicitudAnalisis_ArchivoDA
    {
        [Key]
        [Column(Order = 1)]
        [Required]
        public int TN_IdAnalisis { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required]
        public int TN_IdAchivo { get; set; }

        [Required]
        [StringLength(10)]
        public string TC_TipoArchivo { get; set; }

        [ForeignKey("TN_IdAnalisis")]
        public virtual TSOLITEL_SolicitudAnalisisDA SolicitudAnalisis { get; set; }

        [ForeignKey("TN_IdAchivo")]
        public virtual TSOLITEL_ArchivoDA Archivo { get; set; }
    }

}
