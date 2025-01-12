namespace GestranApi.DTOs.ChecklistItem
{
    public class ChecklistItemDTO
    {
        public int Id { get; set; }
        public int IdChecklist { get; set; }
        public string DescricaoChecklist { get; set; }
        public int IdItem { get; set; }
        public string NomeItem { get; set; }
        public string ObservacaoItem { get; set; }
        public int IdUsuarioAlteracao { get; set; }
        public string NomeUsuarioAlteracao { get; set; }
        public bool Executado { get; set; }
    }
}