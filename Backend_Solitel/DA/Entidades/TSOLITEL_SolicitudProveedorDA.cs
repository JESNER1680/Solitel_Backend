using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    [Table("TSOLITEL_SolicitudProveedor")]
    public class TSOLITEL_SolicitudProveedorDA
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int TN_IdSolicitudProveedor { get; set; }

        public int? TN_NumeroUnico { get; set; }
        public int? TN_NumeroCaso { get; set; }

        [Required]
        [StringLength(150)]
        public string TC_Imputado { get; set; }

        [Required]
        [StringLength(150)]
        public string TC_Ofendido { get; set; }

        [StringLength(255)]
        public string? TC_Resennia { get; set; }

        [Required]
        public int TN_DiasTranscurridos { get; set; }

        [Required]
        public bool TB_Urgente { get; set; }

        [Required]
        public bool TB_Aprobado { get; set; }

        [Required]
        public DateTime TF_FechaCrecion { get; set; }

        [Required]
        public DateTime TF_FechaModificacion { get; set; }

        [Required]
        public int TN_IdUsuarioCreador { get; set; }

        [Required]
        public int TN_IdDelito { get; set; }

        [Required]
        public int TN_IdCategoriaDelito { get; set; }

        [Required]
        public int TN_IdModalida { get; set; }

        [Required]
        public int TN_IdEstado { get; set; }

        [Required]
        public int TN_IdProveedor { get; set; }

        [Required]
        public int TN_IdFiscalia { get; set; }

        [Required]
        public int TN_IdOficina { get; set; }

        [Required]
        public int TN_IdSubModalidad { get; set; }

        [ForeignKey("TN_IdCategoriaDelito")]
        public virtual TSOLITEL_CategoriaDelitoDA CategoriaDelito { get; set; }

        [ForeignKey("TN_IdDelito")]
        public virtual TSOLITEL_DelitoDA Delito { get; set; }

        [ForeignKey("TN_IdEstado")]
        public virtual TSOLITEL_EstadoDA Estado { get; set; }

        [ForeignKey("TN_IdFiscalia")]
        public virtual TSOLITEL_FiscaliaDA Fiscalia { get; set; }

        [ForeignKey("TN_IdModalida")]
        public virtual TSOLITEL_ModalidadDA Modalidad { get; set; }

        [ForeignKey("TN_IdOficina")]
        public virtual TSOLITEL_OficinaDA Oficina { get; set; }

        [ForeignKey("TN_IdProveedor")]
        public virtual TSOLITEL_ProveedorDA Proveedor { get; set; }

        [ForeignKey("TN_IdSubModalidad")]
        public virtual TSOLITEL_SubModalidadDA SubModalidad { get; set; }

        [ForeignKey("TN_IdUsuarioCreador")]
        public virtual TSOLITEL_UsuarioDA UsuarioCreador { get; set; }
    }

}
