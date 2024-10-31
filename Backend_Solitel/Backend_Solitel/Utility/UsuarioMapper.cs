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
                IdUsuario = usuarioDTO.IdUsuario,
                Nombre = usuarioDTO.Nombre,
                Apellido = usuarioDTO.Apellido,
                UsuarioLogin = usuarioDTO.Usuario,
                Contrasenna = "",
                CorreoElectronico = usuarioDTO.CorreoElectronico
            };
        }

        public static UsuarioDTO ToDTO(Usuario usuario)
        {
            return new UsuarioDTO
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Usuario = usuario.UsuarioLogin,
                CorreoElectronico = usuario.CorreoElectronico
            };
        }
    }
}
