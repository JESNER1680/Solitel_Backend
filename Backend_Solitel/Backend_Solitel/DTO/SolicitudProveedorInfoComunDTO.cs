namespace Backend_Solitel.DTO
{
    public class SolicitudProveedorInfoComunDTO
    {
        public FiscaliaDTO Fiscalia { get; set; }

        public DelitoDTO Delito { get; set; }

        public CategoriaDelitoDTO CategoriaDelito { get; set; }

        public string imputado { get; set; }

        public string ofendido { get; set; }

        public string resennia { get; set; }
    }
}
