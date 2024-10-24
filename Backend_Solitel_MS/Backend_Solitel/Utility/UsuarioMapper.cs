using Backend_Solitel.DTO;
using BC.Modelos;

namespace Backend_Solitel.Utility
{
    public static class UsuarioMapper
    {
        public static Usuario ToModel(UsuarioDTO usuarioDTO)
        {
            return new Usuario
            {
                TN_IdUsuario = usuarioDTO.TN_IdUsuario,
                TC_Nombre = usuarioDTO.TC_Nombre,
                TC_Apellido = usuarioDTO.TC_Apellido,
                TC_Usuario = usuarioDTO.TC_Usuario,
                TC_Contrasenna = "",
                TC_CorreoElectronico = usuarioDTO.TC_CorreoElectronico
            };
        }

        public static UsuarioDTO ToDTO(Usuario usuario)
        {
            return new UsuarioDTO
            {
                TN_IdUsuario = usuario.TN_IdUsuario,
                TC_Nombre = usuario.TC_Nombre,
                TC_Apellido = usuario.TC_Apellido,
                TC_Usuario = usuario.TC_Usuario,
                TC_CorreoElectronico = usuario.TC_CorreoElectronico
            };
        }
    }
}
