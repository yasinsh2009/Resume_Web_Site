using Microsoft.EntityFrameworkCore;
using Resume.Domain.Context;
using Resume.Domain.Entities.Common;

namespace Resume.Domain.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        #region Constructor

        private readonly DatabaseContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        #endregion

        #region Implementation

        public IQueryable<TEntity> GetQuery()
        {
            return _dbSet.AsQueryable();
        }

        public async Task AddEntity(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            entity.CreateDate = DateTime.Now;
            entity.UpdateDate = DateTime.Now;
        }

        public async Task GetEntityById(long entityId)
        {
            await _dbSet.SingleOrDefaultAsync(x => x.Id == entityId);
        }

        public void UpdateEntity(TEntity entity)
        {
            _dbSet.Update(entity);
            entity.UpdateDate = DateTime.Now;
        }

        public void DeleteEntity(TEntity entity)
        {
            entity.IsDelete = true;
            UpdateEntity(entity);
        }

        public async Task DeleteEntityById(long entityId)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(x => x.Id == entityId);
            if (entity != null) DeleteEntity(entity);
        }

        public void DeletePermanent(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }


        #endregion

        #region Dispose

        public async ValueTask DisposeAsync()
        {
            if (_context != null)
            {
                await _context.DisposeAsync();
            }
        }

        #endregion
    }
}
