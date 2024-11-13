using BC.Modelos;

namespace Backend_Solitel.DTO
{
    public class RequerimentoAnalisisDTO
    {
        public int IdRequerimientoAnalisis { get; set; }

        public string Objetivo { get; set; }

        public string UtilizadoPor { get; set; }

        public TipoDatoDTO tipoDatoDTO { get; set; }

        public int IdAnalisis { get; set; }
        public CondicionDTO condicion { get; set; }
    }

}
