using GestranApi.DTOs.ChecklistItem;
using GestranApi.Models.Entidades;
namespace GestranApi.Repository.Interface
{
    public interface IChecklistItemRepository : IRepository<ChecklistItem>
    {
        List<ChecklistItemDTO> ListarPorIdChecklist(int IdChecklist);
    }
}