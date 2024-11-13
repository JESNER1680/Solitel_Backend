using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Modelos
{
    public class RequerimentoAnalisis
    {
        public int IdRequerimientoAnalisis { get; set; }

        public string Objetivo { get; set; }

        public string UtilizadoPor { get; set; }

        public TipoDato tipoDato  { get; set; }

        public int IdAnalisis { get; set; }
        public Condicion condicion { get; set; }
    }

}
