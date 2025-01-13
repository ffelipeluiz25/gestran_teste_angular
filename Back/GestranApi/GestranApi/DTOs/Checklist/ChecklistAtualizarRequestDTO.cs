namespace GestranApi.DTOs.Checklist
{
    public class ChecklistAtualizarRequestDTO
    {
        public int Id { get; set; }
        public int IdStatus { get; set; }
        public int IdUsuarioAlteracao { get; set; }
        public string Descricao { get; set; }
    }

    public class ChecklistExecutaRequestDTO
    {
        public int Id { get; set; }
        public int IdStatus { get; set; }
        public int IdUsuarioAlteracao { get; set; }
        public List<ChecklistItemExecucaoDTO> ListaItens { get; set; }
    }

    public class ChecklistItemExecucaoDTO
    {
        public int Id { get; set; }
        public bool Executado { get; set; }
    }
}