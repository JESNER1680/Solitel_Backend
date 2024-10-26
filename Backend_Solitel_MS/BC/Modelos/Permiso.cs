using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entidades
{
    public class Permiso
    {
        public int TN_IdPermiso { get; set; }

        public string TC_Nombre { get; set; }

        public string TC_Descripcion { get; set; }
    }
}
