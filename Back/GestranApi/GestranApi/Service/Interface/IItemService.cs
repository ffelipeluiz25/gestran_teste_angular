using GestranApi.DTOs.Item;
using GestranApi.Models.Entidades;
namespace GestranApi.Service.Interface
{
    public interface IItemService : IBaseServices<Item>
    {
        ItemDTO InserirItem(Item item);
    }
}