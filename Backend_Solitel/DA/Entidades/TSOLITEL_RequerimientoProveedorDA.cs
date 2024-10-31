using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_RequerimientoProveedor")]
    public class TSOLITEL_RequerimientoProveedorDA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TN_IdRequerimientoProveedor { get; set; }

        [Required]
        public DateTime TF_FechaDeInicio { get; set; }

        [Required]
        public DateTime TF_FechaDeFinal { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_Requerimiento { get; set; }

    }

}
