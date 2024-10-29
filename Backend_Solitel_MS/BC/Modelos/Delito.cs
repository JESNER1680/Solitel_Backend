using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Modelos
{
    public class Delito
    {
        public int TN_IdDelito { get; set; }

        public string TC_Nombre { get; set; }

        public string TC_Descripcion { get; set; }

        public int TN_IdCategoriaDelito { get; set; }
    }

}
