namespace GestranApi.DTOs.Usuario
{
    public class UsuarioDTO : BaseDTO
    {
        public string NomeCompleto { get; set; }
        public int IdTipoUsuario { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}