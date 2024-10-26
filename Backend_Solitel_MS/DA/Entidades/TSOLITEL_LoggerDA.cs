using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_Logger")]
    public class TSOLITEL_LoggerDA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TN_IdLogger { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_Accion { get; set; }

        [StringLength(255)]
        public string? TC_DescripcionEvento { get; set; }

        [StringLength(255)]
        public string? TC_InformacionExtra { get; set; }

        [Required]
        public DateTime TF_FechaEvento { get; set; }
    }

}
