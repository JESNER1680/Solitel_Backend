using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_Usuario")]
    public class TSOLITEL_UsuarioDA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TN_IdUsuario { get; set; }

        [Required]
        [StringLength(50)]
        public string TC_Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string TC_Apellido { get; set; }

        [Required]
        [StringLength(50)]
        public string TC_Usuario { get; set; }

        [Required]
        [StringLength(255)]
        public string TC_Contrasenna { get; set; }

        [Required]
        [StringLength(100)]
        public string TC_CorreoElectronico { get; set; }
    }

}
