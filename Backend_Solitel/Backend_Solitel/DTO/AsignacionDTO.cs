namespace Backend_Solitel.DTO
{
    public class AsignacionDTO
    {
        public int IdUsuario { get; set; }

        public int? IdAnalisis { get; set; }

        public int? NumeroSolicitud { get; set; }

        public DateTime FechaModificacion { get; set; }

        public string? NombreUsuario { get; set; }
        public string? DetallesSolicitudAnalisis { get; set; }
        public string? DetallesSolicitudProveedor { get; set; }
    }

}
