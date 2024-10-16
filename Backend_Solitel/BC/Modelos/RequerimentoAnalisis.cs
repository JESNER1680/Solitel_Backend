using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Modelos
{
    public class RequerimentoAnalisis
    {
        public int TN_IdRequerimientoAnalisis { get; set; }

        public string TC_Objetivo { get; set; }

        public string TC_UtilizadoPor { get; set; }

        public int TN_IdTipo { get; set; }

        public int TN_IdAnalisis { get; set; }
    }

}
