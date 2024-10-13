using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_RequerimientoProveedor_DatoRequerido")]
    public class TSOLITEL_RequerimientoProveedor_DatoRequeridoDA
    {
        [Key, Column(Order = 0)]
        [Required]
        public int TN_IdRequerimientoProveedor { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        public int TN_IdDatoRequerido { get; set; }

        [ForeignKey("TN_IdRequerimientoProveedor")]
        public virtual TSOLITEL_RequerimientoProveedorDA RequerimientoProveedor { get; set; }

        [ForeignKey("TN_IdDatoRequerido")]
        public virtual TSOLITEL_DatoRequeridoDA DatoRequerido { get; set; }
    }

}
