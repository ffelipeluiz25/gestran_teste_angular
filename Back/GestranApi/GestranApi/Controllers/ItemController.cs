using AutoMapper;
using GestranApi.DTOs.Item;
using GestranApi.Models.Entidades;
using GestranApi.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace GestranApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IItemService _itemService;
        private readonly ILogger<ItemController> _logger;

        public ItemController(ILogger<ItemController> logger, IItemService itemService, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _itemService = itemService;
        }

        [HttpGet("ListarTodos")]
        [Authorize]
        public IActionResult ListarTodos()
        {
            var listaitem = _itemService.ListarTodos();
            return Ok(listaitem);
        }

        [HttpGet("ListarPorId/{id}")]
        [Authorize]
        public IActionResult ListarPorId(int id)
        {
            var retornoApi = _itemService.ListarPorId(id);
            return Ok(retornoApi);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Salvar(ItemRequestDTO item)
        {
            var retornoApi = _itemService.InserirItem(_mapper.Map<Item>(item));
            return Ok(retornoApi);
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public IActionResult Deletar(int Id)
        {
            _itemService.Deletar(Id);
            return Ok();
        }

    }
}