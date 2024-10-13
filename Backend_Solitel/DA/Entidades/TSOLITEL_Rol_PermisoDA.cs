using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_Rol_Permiso")]
    public class TSOLITEL_Rol_PermisoDA
    {
        [Key, Column(Order = 1)]
        [Required]
        public int TN_IdRol { get; set; }

        [Key, Column(Order = 2)]
        [Required]
        public int TN_IdPermiso { get; set; }

        [ForeignKey("TN_IdRol")]
        public virtual TSOLITEL_RolDA Rol { get; set; }

        [ForeignKey("TN_IdPermiso")]
        public virtual TSOLITEL_PermisoDA Permiso { get; set; }
    }

}
