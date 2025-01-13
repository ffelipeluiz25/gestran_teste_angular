using AutoMapper;
using Azure.Core;
using GestranApi.Context;
using GestranApi.DTOs;
using GestranApi.DTOs.Checklist;
using GestranApi.DTOs.Item;
using GestranApi.Helpers.Enumeradores;
using GestranApi.Models.Entidades;
using GestranApi.Repository.Interface;
using System.Net.NetworkInformation;
namespace GestranApi.Repository
{
    public class ChecklistRepository : Repository<Checklist>, IChecklistRepository
    {
        private readonly IMapper _mapper;
        private readonly IChecklistItemRepository _checklistItemRepository;

        public ChecklistRepository(IMapper mapper, GestranDbContext contexto, IChecklistItemRepository checklistItemRepository) : base(contexto)
        {
            _mapper = mapper;
            _checklistItemRepository = checklistItemRepository;
        }

        public List<ChecklistDTO> ListarChecklist(int IdTipoUsuario, int IdUsuarioLogado)
        {
            var lista = new List<ChecklistDTO>();
            if (IdTipoUsuario.Equals((int)EnumTipoUsuario.Supervisor))
                lista = ListarChecklistSupervisor();
            else if (IdTipoUsuario.Equals((int)EnumTipoUsuario.Executor))
                lista = ListarChecklistExecutor(IdUsuarioLogado);
            return lista;
        }

        private List<ChecklistDTO> ListarChecklistSupervisor()
        {
            var lista = (from c in _contexto.Checklist
                         join s in _contexto.Status on c.IdStatus equals s.Id
                         join u in _contexto.Usuario on c.IdUsuarioExecutor equals u.Id into usuarioGroup
                         from ug in usuarioGroup.DefaultIfEmpty()
                         select new ChecklistDTO()
                         {
                             Id = c.Id,
                             Descricao = c.Descricao,
                             IdUsuarioExecutor = c.IdUsuarioExecutor,
                             IdStatus = c.IdStatus,
                             IdUsuarioAlteracao = c.IdUsuarioAlteracao,
                             Responsavel = ug.NomeCompleto
                         }).ToList();
            return lista;
        }

        private List<ChecklistDTO> ListarChecklistExecutor(int IdUsuarioLogado)
        {
            return (from c in _contexto.Checklist
                    join s in _contexto.Status on c.IdStatus equals s.Id
                    where
                          c.IdUsuarioExecutor.Equals(IdUsuarioLogado)
                           || s.Id.Equals((int)EnumStatus.PENDENTE)
                    select new ChecklistDTO()
                    {
                        Id = c.Id,
                        Descricao = c.Descricao,
                        IdUsuarioExecutor = c.IdUsuarioExecutor,
                        IdStatus = c.IdStatus,
                        IdUsuarioAlteracao = c.IdUsuarioAlteracao
                    }).ToList();
        }

        public RetornoApiDTO AssumeExecucaoChecklist(AssumeExecucaoChecklistRequestDTO request, Checklist checklist)
        {
            try
            {
                _contexto.Checklist.Attach(checklist);

                checklist.IdUsuarioAlteracao = request.IdUsuarioAlteracao;
                checklist.IdUsuarioExecutor = request.IdUsuarioAlteracao;
                checklist.IdStatus = (int)EnumStatus.EXECUTANDO;

                _contexto.Entry(checklist).Property(t => t.IdUsuarioAlteracao).IsModified = true;
                _contexto.Entry(checklist).Property(t => t.IdUsuarioExecutor).IsModified = true;
                _contexto.Entry(checklist).Property(t => t.IdStatus).IsModified = true;

                _contexto.SaveChanges();
                return new RetornoApiDTO(true);
            }
            catch (Exception ex)
            {
                return new RetornoApiDTO(false, ex.Message);
            }
        }

        public RetornoApiDTO AtualizarStatus(ChecklistAtualizarRequestDTO request)
        {
            try
            {
                var checklist = _mapper.Map<Checklist>(request);
                _contexto.Checklist.Attach(checklist);

                _contexto.Entry(checklist).Property(t => t.IdStatus).IsModified = true;
                _contexto.Entry(checklist).Property(t => t.IdUsuarioAlteracao).IsModified = true;

                _contexto.SaveChanges();
                return new RetornoApiDTO(true);
            }
            catch (Exception ex)
            {
                return new RetornoApiDTO(false, ex.Message);
            }
        }


        public RetornoApiDTO Atualizar(ChecklistAtualizarRequestDTO request)
        {
            try
            {
                var checklist = _mapper.Map<Checklist>(request);
                _contexto.Checklist.Attach(checklist);

                _contexto.Entry(checklist).Property(t => t.Descricao).IsModified = true;
                _contexto.Entry(checklist).Property(t => t.IdStatus).IsModified = true;
                _contexto.Entry(checklist).Property(t => t.IdUsuarioAlteracao).IsModified = true;

                _contexto.SaveChanges();

                return new RetornoApiDTO(true);
            }
            catch (Exception ex)
            {
                return new RetornoApiDTO(false, ex.Message);
            }
        }

        public RetornoApiDTO ExecutarChecklist(ChecklistExecutaRequestDTO checklistRequest)
        {
            try
            {
                var checklist = _mapper.Map<Checklist>(checklistRequest);
                _contexto.Checklist.Attach(checklist);

                _contexto.Entry(checklist).Property(t => t.IdStatus).IsModified = true;
                _contexto.Entry(checklist).Property(t => t.IdUsuarioAlteracao).IsModified = true;

                _contexto.SaveChanges();

                if (checklistRequest.ListaItens.Count > 0)
                {
                    foreach (ChecklistItemExecucaoDTO item in checklistRequest.ListaItens)
                    {
                        var checklistItem = _checklistItemRepository.ListarPorId(item.Id);
                        if (checklistItem != null)
                        {
                            _contexto.ChecklistItem.Attach(checklistItem);
                            checklistItem.Executado = item.Executado;
                            _contexto.Entry(checklistItem).Property(t => t.Executado).IsModified = true;
                            _contexto.SaveChanges();
                        }
                    }
                }

                return new RetornoApiDTO(true);
            }
            catch (Exception ex)
            {
                return new RetornoApiDTO(false, ex.Message);
            }
        }
    }
}