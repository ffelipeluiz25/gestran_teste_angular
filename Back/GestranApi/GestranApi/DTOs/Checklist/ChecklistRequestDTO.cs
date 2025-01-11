using GestranApi.DTOs.Item;
namespace GestranApi.DTOs.Checklist
{
    public class ChecklistRequestDTO
    {
        public string Descricao { get; set; }
        public int IdUsuarioAlteracao { get; set; }
        public List<ItemDTO> ListaItens { get; set; }
    }
}