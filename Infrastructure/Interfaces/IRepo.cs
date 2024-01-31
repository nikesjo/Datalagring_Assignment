using System.Linq.Expressions;

namespace Infrastructure.Interfaces;

public interface IRepo<TEntity> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
    Task<IEnumerable<TEntity>> GetAsync();
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity entity);
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
}
