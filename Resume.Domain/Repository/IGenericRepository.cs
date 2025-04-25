using Resume.Domain.Entities.Common;

namespace Resume.Domain.Repository
{
    public interface IGenericRepository<TEntity> : IAsyncDisposable where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetQuery();
        Task AddEntity(TEntity entity);
        Task GetEntityById(long entityId);
        void UpdateEntity(TEntity entity);
        void DeleteEntity(TEntity entity);
        Task DeleteEntityById(long entityId);
        void DeletePermanent(TEntity entity);
        Task SaveChanges();
    }
}
