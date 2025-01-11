using GestranApi.DTOs.Checklist;
using GestranApi.Models.Entidades;
namespace GestranApi.Service.Interface
{
    public interface IChecklistService : IBaseServices<Checklist>
    {
        List<ChecklistDTO> ListarChecklist(int IdTipoUsuario, int IdUsuarioLogado);
        ChecklistDTO InserirChecklist(ChecklistRequestDTO checklist);
    }
}