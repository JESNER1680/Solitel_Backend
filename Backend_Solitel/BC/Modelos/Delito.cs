﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Modelos
{
    public class Delito
    {
        public int IdDelito { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int IdCategoriaDelito { get; set; }
    }

}
