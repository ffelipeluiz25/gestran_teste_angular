using GestranApi.Context;
using GestranApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace GestranApi.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly GestranDbContext _contexto;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(GestranDbContext contexto)
        {
            _contexto = contexto;
            DbSet = _contexto.Set<TEntity>();
        }

        public virtual void Inserir(TEntity obj)
        {
            DbSet.Add(obj);
            SaveChanges();
        }

        public virtual void Inserir(IEnumerable<TEntity> entity)
        {
            DbSet.AddRange(entity);
            SaveChanges();
        }

        public virtual TEntity ListarPorId(int id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> ListarPor(Expression<Func<TEntity, bool>> expressao)
        {
            return ListarTodos().Where(expressao).AsQueryable();
        }

        public bool Existe(Expression<Func<TEntity, bool>> expressao)
        {
            return DbSet.Any(expressao);
        }

        public virtual void Atualizar(TEntity obj)
        {
            _contexto.Entry(obj).State = EntityState.Modified;
            DbSet.Update(obj);
            SaveChanges();

        }

        public virtual void Atualizar(IEnumerable<TEntity> entity)
        {
            DbSet.UpdateRange(entity);
            SaveChanges();

        }

        public virtual void Deletar(int id)
        {
            DbSet.Remove(DbSet.Find(id));
            SaveChanges();

        }

        public virtual void Deletar(IEnumerable<TEntity> entity)
        {
            DbSet.RemoveRange(entity);
            SaveChanges();

        }

        public int SaveChanges()
        {
            return _contexto.SaveChanges();
        }

        public void Dispose()
        {
            _contexto.Dispose();
            GC.SuppressFinalize(this);
        }

        public IQueryable<TEntity> ListarTodos()
        {
            return DbSet;
        }
    }
}