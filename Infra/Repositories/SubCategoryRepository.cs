using Domain.Entities;
using Domain.Repositories;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;
public class SubCategoryRepository : BaseRepository<SubCategory>, ISubCategoryRepository
{
  private readonly ApplicationContext _context;

  public SubCategoryRepository(ApplicationContext context) : base(context)
  {
    _context = context;
  }

  public new async Task<List<SubCategory>> GetAll()
  {
    return await _context.SubCategories
      .Include(s => s.Category)
      .AsNoTracking()
      .ToListAsync();
  }

  public new async Task<SubCategory> GetById(Guid id)
  {
    return await _context.SubCategories
      .Include(s => s.Category)
      .FirstOrDefaultAsync(s => s.Id == id);
  }

  public async Task<List<SubCategory>> GetByCategoryId(Guid id)
  {
    return await _context.SubCategories
      .Include(s => s.Category)
      .Where(s => s.CategoryId == id)
      .ToListAsync();
  }
}
