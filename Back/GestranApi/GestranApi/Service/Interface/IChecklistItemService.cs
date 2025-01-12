using GestranApi.DTOs.ChecklistItem;
using GestranApi.Models.Entidades;
namespace GestranApi.Service.Interface
{
    public interface IChecklistItemService : IBaseServices<ChecklistItem>
    {
        List<ChecklistItemDTO> ListarPorIdChecklist(int IdChecklist);
    }
}