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
        public List<RequerimientoProveedorDTO> Requerimientos { get; set; }
        public List<ProveedorDTO> Operadoras { get; set; }
        public UsuarioDTO UsuarioCreador { get; set; }
        public DelitoDTO Delito { get; set; }
        public CategoriaDelitoDTO CategoriaDelito { get; set; }
        public EstadoDTO Estado { get; set; }
        public FiscaliaDTO Fiscalia { get; set; }
        public OficinaDTO Oficina { get; set; }
        public ModalidadDTO Modalidad { get; set; }
        public SubModalidadDTO SubModalidad { get; set; }

    }

}
