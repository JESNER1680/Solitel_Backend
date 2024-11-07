namespace Backend_Solitel.DTO
{
    public class SolicitudProveedorInfoComunDTO
    {
        public FiscaliaDTO FiscaliaDTO { get; set; }

        public DelitoDTO DelitoDTO { get; set; }

        public CategoriaDelitoDTO CategoriaDelitoDTO { get; set; }

        public string imputado { get; set; }

        public string ofendido { get; set; }

        public string resennia { get; set; }
    }
}
