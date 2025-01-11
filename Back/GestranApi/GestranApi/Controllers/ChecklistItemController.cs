using GestranApi.Models.Entidades;
using GestranApi.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
namespace GestranApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChecklistItemController : ControllerBase
    {

        private readonly IChecklistItemService _checklistItemService;
        private readonly ILogger<ChecklistItemController> _logger;

        public ChecklistItemController(ILogger<ChecklistItemController> logger, IChecklistItemService checklistItemService)
        {
            _logger = logger;
            _checklistItemService = checklistItemService;
        }

        [HttpGet("ListarTodos")]
        [Authorize]
        public IActionResult ListarTodos()
        {
            var retornoApi = _checklistItemService.ListarTodos();
            return Ok(retornoApi);
        }

        [HttpGet("ListarPorId/{id}")]
        [Authorize]
        public IActionResult ListarPorId(int id)
        {
            var retornoApi = _checklistItemService.ListarPorId(id);
            return Ok(retornoApi);
        }

        [HttpGet("ListarPor")]
        [Authorize]
        public IActionResult ListarPor(Expression<Func<ChecklistItem, bool>> expressao)
        {
            var retornoApi = _checklistItemService.ListarPor(expressao);
            return Ok(retornoApi);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Salvar(ChecklistItem checklistItem)
        {
            var retornoApi = _checklistItemService.Salvar(checklistItem);
            return Ok(retornoApi);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Atualizar(ChecklistItem checklistItem)
        {
            var retornoApi = _checklistItemService.Atualizar(checklistItem);
            return Ok(retornoApi);
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public IActionResult Deletar(int Id)
        {
            _checklistItemService.Deletar(Id);
            return Ok();
        }

    }
}