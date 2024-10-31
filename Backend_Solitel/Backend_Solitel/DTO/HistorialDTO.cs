namespace Backend_Solitel.DTO
{
    public class HistorialDTO
    {
        public int TN_IdHistorial { get; set; }

        public string TC_Observacion { get; set; }

        public DateTime TF_FechaEstado { get; set; }

        public UsuarioDTO usuarioDTO { get; set; }

        public EstadoDTO estadoDTO { get; set; }

        public int? TN_IdAnalisis { get; set; }

        public int? TN_IdSolicitudProveedor { get; set; }
    }

}
