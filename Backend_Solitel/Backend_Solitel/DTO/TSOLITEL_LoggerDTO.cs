namespace Backend_Solitel.DTO
{
    public class TSOLITEL_LoggerDTO
    {
        public int TN_IdLogger { get; set; }

        public string TC_Accion { get; set; }

        public string? TC_DescripcionEvento { get; set; }

        public string? TC_InformacionExtra { get; set; }

        public DateTime TF_FechaEvento { get; set; }
    }

}
