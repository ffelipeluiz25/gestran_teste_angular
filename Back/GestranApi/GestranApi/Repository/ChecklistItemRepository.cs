using GestranApi.Context;
using GestranApi.DTOs.ChecklistItem;
using GestranApi.Models.Entidades;
using GestranApi.Repository.Interface;
namespace GestranApi.Repository
{
    public class ChecklistItemRepository : Repository<ChecklistItem>, IChecklistItemRepository
    {
        public ChecklistItemRepository(GestranDbContext contexto) : base(contexto)
        {
        }

        public List<ChecklistItemDTO> ListarPorIdChecklist(int IdChecklist)
        {
            var lista = (from chi in _contexto.ChecklistItem
                         join c in _contexto.Checklist on chi.IdChecklist equals c.Id
                         join i in _contexto.Item on chi.IdItem equals i.Id
                         join u in _contexto.Usuario on chi.IdUsuarioAlteracao equals u.Id
                         where c.Id.Equals(IdChecklist)
                         select new ChecklistItemDTO()
                         {
                             Id = chi.Id,
                             IdChecklist = chi.IdChecklist,
                             DescricaoChecklist = c.Descricao,
                             IdItem = chi.IdItem,
                             NomeItem = i.Nome,
                             ObservacaoItem = i.Observacao,
                             IdUsuarioAlteracao = u.Id,
                             NomeUsuarioAlteracao = u.NomeCompleto,
                             Executado = chi.Executado
                         }).ToList();
            return lista;
        }

        public ChecklistItem ListarPorIdChecklistPorIdItem(int IdChecklist, int IdItem)
        {
            var checklistItem = (from chi in _contexto.ChecklistItem
                                 where
                                    chi.IdChecklist.Equals(IdChecklist)
                                    && chi.IdItem.Equals(IdItem)
                                 select chi).FirstOrDefault();
            return checklistItem;
        }
    }
}