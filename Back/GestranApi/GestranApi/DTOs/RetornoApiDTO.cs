namespace GestranApi.DTOs
{
    public class RetornoApiDTO
    {
        public int Status { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public string IdMensagem { get; set; }
        public dynamic Dados { get; set; }

        public RetornoApiDTO()
        {
        }

        public RetornoApiDTO(bool sucesso)
        {
            Sucesso = sucesso;
        }

        public RetornoApiDTO(bool sucesso, string mensagem, string idMensagem = "")
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            IdMensagem = idMensagem;
        }

        public RetornoApiDTO(string mensagem, string idMensagem = "")
        {
            Sucesso = false;
            Mensagem = mensagem;
            IdMensagem = idMensagem;
        }

        public RetornoApiDTO(dynamic dados)
        {
            Sucesso = true;
            Dados = dados;
        }
    }

    public class RetornoApiDTO<T> : RetornoApiDTO
    {
        public T Dados { get; set; }

        public RetornoApiDTO(bool sucesso, string mensagem) : base(sucesso, mensagem)
        {
        }
        public RetornoApiDTO(T dados) : base(dados)
        {
            Dados = dados;
            Sucesso = true;
        }
    }
}
