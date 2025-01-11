using AutoMapper;
using GestranApi.DTOs.Item;
using GestranApi.Models.Entidades;
using GestranApi.Repository.Interface;
using GestranApi.Service.Interface;
namespace GestranApi.Service
{
    public class ItemService : BaseServices<Item>, IItemService
    {
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IMapper mapper) : base(itemRepository)
        {
            _mapper = mapper;
        }

        public ItemDTO InserirItem(Item item)
        {
            _repository.Inserir(item);
            _repository.SaveChanges();
            return _mapper.Map<ItemDTO>(item);
        }

    }
}