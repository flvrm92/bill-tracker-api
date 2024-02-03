using Domain.Entities;
using Domain.Repositories;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
  public class BaseRepository<T>(ApplicationContext context) : IRepository<T> where T : BaseEntity
  {
    public IQueryable<T> GetAll()
    {
      return context.Set<T>().AsQueryable();
    }

    public async Task<T> GetById(Guid id)
    {
      return await GetAll().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<T> Add(T entity)
    {
      context.Set<T>().Add(entity);
      await context.SaveChangesAsync();
      return entity;
    }

    public async Task<T> Update(T entity)
    {
      context.Set<T>().Update(entity);
      await context.SaveChangesAsync();
      return entity;
    }

    public async Task Delete(T entity)
    {
      context.Set<T>().Remove(entity);
      await context.SaveChangesAsync();
    }
  }
}
