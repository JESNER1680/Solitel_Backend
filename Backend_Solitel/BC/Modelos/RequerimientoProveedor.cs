using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Modelos
{
    public class RequerimientoProveedor
    {
        public int TN_IdRequerimientoProveedor { get; set; }

        public DateTime TF_FechaInicio { get; set; }

        public DateTime TF_FechaFinal { get; set; }

        public string TC_Requerimiento { get; set; }

        public int TN_NumeroSolicitud { get; set; }
    }

}
