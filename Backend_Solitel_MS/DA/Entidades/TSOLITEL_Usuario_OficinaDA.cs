using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_Usuario_Oficina")]
    public class TSOLITEL_Usuario_OficinaDA
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Usuario")]
        public int TN_IdUsuario { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Oficina")]
        public int TN_IdOficina { get; set; }

        [Required]
        [ForeignKey("Rol")]
        public int TN_IdRol { get; set; }
        public virtual TSOLITEL_UsuarioDA Usuario { get; set; }
        public virtual TSOLITEL_OficinaDA Oficina { get; set; }
        public virtual TSOLITEL_RolDA Rol { get; set; }
    }

}
