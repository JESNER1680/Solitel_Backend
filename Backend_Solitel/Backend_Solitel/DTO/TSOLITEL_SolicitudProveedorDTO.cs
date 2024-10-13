namespace Backend_Solitel.DTO
{
    public class TSOLITEL_SolicitudProveedorDTO
    {
        public int TN_IdSolicitudProveedor { get; set; }
        public int? TN_NumeroUnico { get; set; }
        public int? TN_NumeroCaso { get; set; }
        public string TC_Imputado { get; set; }
        public string TC_Ofendido { get; set; }
        public string? TC_Resennia { get; set; }
        public int TN_DiasTranscurridos { get; set; }
        public bool TB_Urgente { get; set; }
        public bool TB_Aprobado { get; set; }
        public DateTime TF_FechaCrecion { get; set; }
        public DateTime TF_FechaModificacion { get; set; }
        public int TN_IdUsuarioCreador { get; set; }
        public int TN_IdDelito { get; set; }
        public int TN_IdCategoriaDelito { get; set; }
        public int TN_IdModalida { get; set; }
        public int TN_IdEstado { get; set; }
        public int TN_IdProveedor { get; set; }
        public int TN_IdFiscalia { get; set; }
        public int TN_IdOficina { get; set; }
        public int TN_IdSubModalidad { get; set; }
    }

}
