using GestranApi.DTOs;
using GestranApi.DTOs.Checklist;
using GestranApi.Models.Entidades;
namespace GestranApi.Repository.Interface
{
    public interface IChecklistRepository : IRepository<Checklist>
    {
        RetornoApiDTO AssumeExecucaoChecklist(AssumeExecucaoChecklistRequestDTO request, Checklist checklist);
        RetornoApiDTO Atualizar(ChecklistAtualizarRequestDTO request);
        RetornoApiDTO AtualizarStatus(ChecklistAtualizarRequestDTO checklistRequest);
        RetornoApiDTO ExecutarChecklist(ChecklistExecutaRequestDTO checklistRequest);
        List<ChecklistDTO> ListarChecklist(int IdTipoUsuario, int IdUsuarioLogado);
    }
}