using GestranApi.Models.Entidades;
using GestranApi.Repository.Interface;
using GestranApi.Service.Interface;
namespace GestranApi.Service
{
    public class ChecklistItemService : BaseServices<ChecklistItem>, IChecklistItemService
    {

        public ChecklistItemService(IChecklistItemRepository checklistItemServiceRepository) : base(checklistItemServiceRepository)
        {
        }

    }
}