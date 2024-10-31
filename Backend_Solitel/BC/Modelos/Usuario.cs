using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Modelos
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UsuarioLogin { get; set; }
        public string Contrasenna { get; set; }
        public string CorreoElectronico { get; set; }
    }

}
