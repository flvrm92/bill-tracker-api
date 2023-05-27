using Domain.Entities;
using Domain.Repositories;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
  public class BaseRepository<T>: IRepository<T> where T : BaseEntity
  {
    private readonly ApplicationContext _context;

    public BaseRepository(ApplicationContext context)
    {
      _context = context;
    }

    public IQueryable<T> GetAll()
    {
      return _context.Set<T>().AsQueryable();
    }

    public async Task<T> GetById(Guid id)
    {
      return await GetAll().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<T> Add(T entity)
    {
      _context.Set<T>().Add(entity);
      await _context.SaveChangesAsync();
      return entity;
    }

    public async Task<T> Update(T entity)
    {
      _context.Set<T>().Update(entity);
      await _context.SaveChangesAsync();
      return entity;
    }

    public async Task Delete(T entity)
    {
      _context.Set<T>().Remove(entity);
      await _context.SaveChangesAsync();
    }
  }
}
