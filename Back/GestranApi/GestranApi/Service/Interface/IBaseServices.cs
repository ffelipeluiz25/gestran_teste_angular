using System.Linq.Expressions;
namespace GestranApi.Service.Interface
{
    public interface IBaseServices<TEntity> where TEntity : class
    {
        TEntity Salvar(TEntity obj);
        TEntity Atualizar(TEntity obj);
        void Deletar(int id);
        IEnumerable<TEntity> ListarTodos();
        TEntity ListarPorId(int id);
        IEnumerable<TEntity> ListarPor(Expression<Func<TEntity, bool>> expressao);
    }
}