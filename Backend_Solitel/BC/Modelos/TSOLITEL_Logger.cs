using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Modelos
{
    public class TSOLITEL_Logger
    {
        public int TN_IdLogger { get; set; }

        public string TC_Accion { get; set; }

        public string? TC_DescripcionEvento { get; set; }

        public string? TC_InformacionExtra { get; set; }

        public DateTime TF_FechaEvento { get; set; }
    }

}
