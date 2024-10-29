namespace BC.Modelos
{
    public class SolicitudProveedor
    {
        public int IdSolicitudProveedor { get; set; }
        public int NumeroUnico { get; set; }
        public int NumeroCaso { get; set; }
        public string Imputado { get; set; }
        public string Ofendido { get; set; }
        public string? Resennia { get; set; }
        public int DiasTranscurridos { get; set; }
        public bool Urgente { get; set; }
        public bool Aprobado { get; set; }
        public DateTime FechaCrecion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public Usuario UsuarioCreador { get; set; }
        public Delito Delito { get; set; }
        public CategoriaDelito CategoriaDelito { get; set; }
        public Estado Estado { get; set; }
        public Fiscalia Fiscalia { get; set; }
        public Oficina Oficina { get; set; }
        public Modalidad Modalidad { get; set; }
        public SubModalidad SubModalidad { get; set; }
        public Proveedor Proveedor { get; set; }

    }

}
