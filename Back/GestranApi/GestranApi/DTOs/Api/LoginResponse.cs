namespace GestranApi.DTOs.Api
{
    public class LoginResponse
    {
        public int IdUsuarioLogado { get; set; }
        public string NomeUsuario { get; set; }
        public string TipoUsuario { get; set; }
        public string? AcessToken { get; set; }
    }
}