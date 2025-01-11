using GestranApi.Context;
using GestranApi.Models.Entidades;
using GestranApi.Repository.Interface;
namespace GestranApi.Repository
{
    public class ChecklistItemRepository : Repository<ChecklistItem>, IChecklistItemRepository
    {
        public ChecklistItemRepository(GestranDbContext contexto) : base(contexto)
        {
        }
    }
}