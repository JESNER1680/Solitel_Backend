using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Modelos
{
    public class TSOLITEL_Historial
    {
        public int TN_IdHistorial { get; set; }

        public string TC_Observacion { get; set; }

        public DateTime TF_FechaEstado { get; set; }

        public int TN_IdUsuario { get; set; }

        public int TN_IdEstado { get; set; }

        public int? TN_IdAnalisis { get; set; }

        public int? TN_NumeroSolicitud { get; set; }
    }

}
