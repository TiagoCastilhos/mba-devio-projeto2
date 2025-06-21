using System.Linq.Expressions;
using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Models.Base;
using DevXpert.Store.Core.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevXpert.Store.Core.Data.Repositories
{
    public abstract class Repository<TEntity>(AppDbContext db) : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly AppDbContext Db = db;
        protected readonly DbSet<TEntity> DbSet = db.Set<TEntity>();

        #region READ
        public virtual async Task<IEnumerable<TEntity>> Pesquisar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate)
                              .AsNoTracking()
                              .ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> BuscarTodos()
        {
            return await DbSet.AsNoTracking()
                              .ToListAsync();
        }        

        public virtual async Task<TEntity> BuscarPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }
        #endregion

        #region WRITE
        public virtual async Task<int> Adicionar(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            return 1;
        }

        public virtual async Task<int> Atualizar(TEntity entity)
        {
            var e = await DbSet.FindAsync(entity.Id);
            Db.Entry(e).CurrentValues.SetValues(entity);
            return 1;
        }

        public virtual async Task<int> Excluir(Guid id)
        {
            var e = await DbSet.FindAsync(id);
            DbSet.Remove(e);
            return 1;
        }

        public virtual async Task<int> Salvar()
        {
            return await Db.SaveChangesAsync();
        }
        #endregion

        #region DISPOSE
        public void Dispose()
        {
            Db?.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}