namespace GestranApi.DTOs.Item
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Observacao { get; set; }
        public int IdUsuarioAlteracao { get; set; }
    }
}