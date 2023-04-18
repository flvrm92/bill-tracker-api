using Domain.Entities;

namespace Domain.Repositories;
public interface IRepository<TEntity> where TEntity : BaseEntity
{
  IQueryable<TEntity> GetAll();
  Task<TEntity> GetById(Guid id);
  Task<TEntity> Add(TEntity entity);
  Task<TEntity> Update(TEntity entity);
  Task Delete(TEntity entity);
}
