using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Modelos
{
    public class CategoriaDelito
    {
        public int TN_IdCategoriaDelito { get; set; }

        public string TC_Nombre { get; set; }

        public string TC_Descripcion { get; set; }
    }
}
