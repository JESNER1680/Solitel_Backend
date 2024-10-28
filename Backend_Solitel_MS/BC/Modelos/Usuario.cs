using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Modelos
{
    public class Usuario
    {
        public int TN_IdUsuario { get; set; }

        public string TC_Nombre { get; set; }

        public string TC_Apellido { get; set; }

        public string TC_Usuario { get; set; }

        public string TC_Contrasenna { get; set; }

        public string TC_CorreoElectronico { get; set; }
    }
}
