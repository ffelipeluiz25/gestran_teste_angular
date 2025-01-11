using GestranApi.Repository.Interface;
using GestranApi.Service.Interface;
using System.Linq.Expressions;
namespace GestranApi.Service
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class
    {
        protected readonly IRepository<TEntity> _repository;

        public BaseServices(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public TEntity Salvar(TEntity obj)
        {
            _repository.Inserir(obj);
            _repository.SaveChanges();
            return obj;
        }

        public virtual TEntity Atualizar(TEntity obj)
        {
            _repository.Atualizar(obj);
            _repository.SaveChanges();
            return obj;
        }

        public virtual void Deletar(int id)
        {
            _repository.Deletar(id);
            _repository.SaveChanges();
        }

        public void DeleteList(IEnumerable<TEntity> objs)
        {
            _repository.Deletar(objs);
            _repository.SaveChanges();
        }

        public virtual IEnumerable<TEntity> ListarTodos()
        {
            return _repository.ListarTodos();
        }

        public TEntity ListarPorId(int id)
        {
            return _repository.ListarPorId(id);
        }

        public IEnumerable<TEntity> ListarPor(Expression<Func<TEntity, bool>> expressao)
        {
            return _repository.ListarPor(expressao);
        }
    }
}