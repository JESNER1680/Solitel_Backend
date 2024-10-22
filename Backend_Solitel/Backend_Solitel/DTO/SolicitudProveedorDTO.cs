using BC.Modelos;

namespace Backend_Solitel.DTO
{
    public class SolicitudProveedorDTO
    {
        public int IdSolicitudProveedor { get; set; }
        public int NumeroUnico { get; set; }
        public int NumeroCaso { get; set; }
        public string Imputado { get; set; }
        public string Ofendido { get; set; }
        public string? Resennia { get; set; }
        public bool Urgente { get; set; }
        public List<RequerimientoProveedorDTO> requerimientos { get; set; }
        public List<ProveedorDTO> operadoras { get; set; }
        public int IdUsuarioCreador { get; set; }
        public int IdDelito { get; set; }
        public int IdCategoriaDelito { get; set; }
        public int IdEstado { get; set; }
        public int IdFiscalia { get; set; }
        public int IdOficina { get; set; }
        public int IdModalida { get; set; }
        public int IdSubModalidad { get; set; }

        //public bool TB_Aprobado { get; set; }
        //public DateTime TF_FechaCrecion { get; set; }
        //public DateTime TF_FechaModificacion { get; set; }
        //public int TN_DiasTranscurridos { get; set; }
        //public int TN_IdProveedor { get; set; }
    }

}
