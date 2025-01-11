using GestranApi.DTOs.Checklist;
using System.Linq.Expressions;
namespace GestranApi.Repository.Interface
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Inserir(TEntity obj);
        void Inserir(IEnumerable<TEntity> entity);
        TEntity ListarPorId(int id);
        IQueryable<TEntity> ListarTodos();
        IQueryable<TEntity> ListarPor(Expression<Func<TEntity, bool>> expressao);
        bool Existe(Expression<Func<TEntity, bool>> expressao);
        void Atualizar(TEntity obj);
        void Atualizar(IEnumerable<TEntity> entity);
        void Deletar(int id);
        void Deletar(IEnumerable<TEntity> entity);
        int SaveChanges();
    }
}