using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Modelos
{
    public class Oficina
    {
        public int TN_IdOficina { get; set; }

        public string TC_Nombre { get; set; }
        public string TC_Tipo { get; set; } 
    }

}
