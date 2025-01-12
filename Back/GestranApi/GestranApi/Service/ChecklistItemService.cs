using GestranApi.DTOs.ChecklistItem;
using GestranApi.Models.Entidades;
using GestranApi.Repository.Interface;
using GestranApi.Service.Interface;

namespace GestranApi.Service
{
    public class ChecklistItemService : BaseServices<ChecklistItem>, IChecklistItemService
    {
        private readonly IChecklistItemRepository _checklistItemServiceRepository;
        public ChecklistItemService(IChecklistItemRepository checklistItemServiceRepository) : base(checklistItemServiceRepository)
        {
            _checklistItemServiceRepository = checklistItemServiceRepository;
        }

        public List<ChecklistItemDTO> ListarPorIdChecklist(int IdChecklist)
        {
            return _checklistItemServiceRepository.ListarPorIdChecklist(IdChecklist);
        }
    }
}