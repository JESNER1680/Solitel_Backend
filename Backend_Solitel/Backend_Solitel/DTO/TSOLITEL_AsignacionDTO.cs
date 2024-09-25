namespace Backend_Solitel.DTO
{
    public class TSOLITEL_AsignacionDTO
    {
        public int TN_IdUsuario { get; set; }

        public int? TN_IdAnalisis { get; set; }

        public int? TN_NumeroSolicitud { get; set; }

        public DateTime TF_FechaModificacion { get; set; }

        public string? NombreUsuario { get; set; }
        public string? DetallesSolicitudAnalisis { get; set; }
        public string? DetallesSolicitudProveedor { get; set; }
    }

}
