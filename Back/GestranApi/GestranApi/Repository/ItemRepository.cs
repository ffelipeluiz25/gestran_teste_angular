using GestranApi.Context;
using GestranApi.Models.Entidades;
using GestranApi.Repository.Interface;
namespace GestranApi.Repository
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(GestranDbContext contexto) : base(contexto)
        {
        }
    }
}