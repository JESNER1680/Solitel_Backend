using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Modelos
{
    public class Historial
    {
        public int IdHistorial { get; set; }

        public string Observacion { get; set; }

        public DateTime FechaEstado { get; set; }

        public Usuario usuario { get; set; }

        public Estado estado { get; set; }

        public int? IdAnalisis { get; set; }

        public int? IdSolicitudProveedor { get; set; }
    }

}
