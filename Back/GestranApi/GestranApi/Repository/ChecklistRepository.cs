using GestranApi.Context;
using GestranApi.DTOs;
using GestranApi.DTOs.Checklist;
using GestranApi.Helpers.Enumeradores;
using GestranApi.Models.Entidades;
using GestranApi.Repository.Interface;
using System.Net.NetworkInformation;
namespace GestranApi.Repository
{
    public class ChecklistRepository : Repository<Checklist>, IChecklistRepository
    {
        public ChecklistRepository(GestranDbContext contexto) : base(contexto)
        {
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
                         ((s.Id.Equals((int)EnumStatus.EXECUTANDO) && c.IdUsuarioExecutor.Equals(IdUsuarioLogado))
                           || s.Id.Equals((int)EnumStatus.PENDENTE))
                    select new ChecklistDTO()
                    {
                        Id = c.Id,
                        Descricao = c.Descricao,
                        IdUsuarioExecutor = c.IdUsuarioExecutor,
                        IdStatus = c.IdStatus,
                        IdUsuarioAlteracao = c.IdUsuarioAlteracao
                    }).ToList();
        }

        public RetornoApiDTO AssumeExecucaoChecklist(AssumeExecucaoChecklistRequestDTO request)
        {
            try
            {
                var checklist = new Checklist
                {
                    Id = request.Id,
                    IdUsuarioAlteracao = request.IdUsuarioAlteracao,
                    IdUsuarioExecutor = request.IdUsuarioAlteracao,
                    IdStatus = (int)EnumStatus.EXECUTANDO
                };

                _contexto.Checklist.Attach(checklist);
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

    }
}