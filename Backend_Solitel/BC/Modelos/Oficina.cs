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
        public int IdOficina { get; set; }

        public string Nombre { get; set; }
        public string Tipo { get; set; } 
    }

}
