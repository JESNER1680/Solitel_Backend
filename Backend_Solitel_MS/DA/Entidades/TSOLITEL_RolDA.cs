using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_Rol")]
    public class TSOLITEL_RolDA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int TN_IdRol { get; set; }

        [Required]
        [StringLength(50)]
        public string TC_Nombre { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_Descripcion { get; set; }

        public virtual List<TSOLITEL_PermisoDA> Permisos { get; set; }
    }
}
