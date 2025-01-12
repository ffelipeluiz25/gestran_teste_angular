using AutoMapper;
using GestranApi.DTOs.Checklist;
using GestranApi.Models.Entidades;
using GestranApi.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace GestranApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChecklistController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IChecklistService _checklistService;
        private readonly ILogger<ChecklistController> _logger;

        public ChecklistController(ILogger<ChecklistController> logger, IChecklistService checklistService, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _checklistService = checklistService;
        }

        [HttpGet("ListarChecklist")]
        [Authorize]
        public IActionResult ListarChecklist(int IdTipoUsuario, int IdUsuarioLogado)
        {
            var listaChecklist = _checklistService.ListarChecklist(IdTipoUsuario, IdUsuarioLogado);
            return Ok(listaChecklist);
        }

        [HttpGet("ListarTodos")]
        [Authorize]
        public IActionResult ListarTodos()
        {
            var listaChecklist = _checklistService.ListarTodos();
            return Ok(listaChecklist);
        }

        [HttpGet("ListarPorId/{id}")]
        [Authorize]
        public IActionResult ListarPorId(int id)
        {
            var retornoApi = _checklistService.ListarPorId(id);
            return Ok(retornoApi);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Salvar(ChecklistRequestDTO checklist)
        {
            var retornoApi = _checklistService.InserirChecklist(checklist);
            return Ok(retornoApi);
        }


        [HttpPost("AssumeExecucaoChecklist")]
        [Authorize]
        public IActionResult AssumeExecucaoChecklist(AssumeExecucaoChecklistRequestDTO request)
        {
            var retornoApi = _checklistService.AssumeExecucaoChecklist(request);
            return Ok(retornoApi);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Atualizar(Checklist checklist)
        {
            var retornoApi = _checklistService.Atualizar(checklist);
            return Ok(retornoApi);
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public IActionResult Deletar(int Id)
        {
            _checklistService.Deletar(Id);
            return Ok();
        }

    }
}