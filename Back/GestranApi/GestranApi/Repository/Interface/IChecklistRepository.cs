using GestranApi.DTOs.Checklist;
using GestranApi.Models.Entidades;
namespace GestranApi.Repository.Interface
{
    public interface IChecklistRepository : IRepository<Checklist>
    {
        List<ChecklistDTO> ListarChecklist(int IdTipoUsuario, int IdUsuarioLogado);
    }
}