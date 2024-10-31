namespace Backend_Solitel.DTO
{
    public class HistorialDTO
    {
        public int IdHistorial { get; set; }

        public string Observacion { get; set; }

        public DateTime FechaEstado { get; set; }

        public UsuarioDTO usuarioDTO { get; set; }

        public EstadoDTO estadoDTO { get; set; }

        public int? IdAnalisis { get; set; }

        public int? IdSolicitudProveedor { get; set; }
    }

}
