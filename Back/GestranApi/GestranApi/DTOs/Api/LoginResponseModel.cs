namespace GestranApi.DTOs.Api
{
    public class LoginResponseModel
    {
        public string? NomeUsuario { get; set; }
        public string? AcessToken { get; set; }
        public int ExpiraEmSegundos { get; set; }
    }
}