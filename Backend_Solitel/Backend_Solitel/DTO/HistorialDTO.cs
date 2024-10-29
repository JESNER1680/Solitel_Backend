namespace Backend_Solitel.DTO
{
    public class HistorialDTO
    {
        public int TN_IdHistorial { get; set; }

        public string TC_Observacion { get; set; }

        public DateTime TF_FechaEstado { get; set; }

        public int TN_IdUsuario { get; set; }

        public int TN_IdEstado { get; set; }

        public int? TN_IdAnalisis { get; set; }

        public int? TN_IdSolicitudProveedor { get; set; }
    }

}
