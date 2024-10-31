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
                IdUsuario = usuarioDTO.TN_IdUsuario,
                Nombre = usuarioDTO.TC_Nombre,
                Apellido = usuarioDTO.TC_Apellido,
                UsuarioLogin = usuarioDTO.TC_Usuario,
                Contrasenna = "",
                CorreoElectronico = usuarioDTO.TC_CorreoElectronico
            };
        }

        public static UsuarioDTO ToDTO(Usuario usuario)
        {
            return new UsuarioDTO
            {
                TN_IdUsuario = usuario.IdUsuario,
                TC_Nombre = usuario.Nombre,
                TC_Apellido = usuario.Apellido,
                TC_Usuario = usuario.UsuarioLogin,
                TC_CorreoElectronico = usuario.CorreoElectronico
            };
        }
    }
}
