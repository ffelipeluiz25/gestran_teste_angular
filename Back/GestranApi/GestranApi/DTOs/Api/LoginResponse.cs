namespace GestranApi.DTOs.Api
{
    public class LoginResponse
    {
        public string TipoUsuario { get; set; }
        public string? AcessToken { get; set; }
    }
}