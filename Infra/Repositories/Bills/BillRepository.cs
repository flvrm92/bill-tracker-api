using Domain.Entities.Bills;
using Domain.Repositories.Bills;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories.Bills;

public class BillRepository(ApplicationContext context) : BaseRepository<Bill>(context), IBillRepository
{
  private readonly ApplicationContext _context = context;

  public new async Task<Bill> GetById(Guid id)
  {
    return await _context.Bills
      .Include(b => b.BillItems)
      .FirstOrDefaultAsync(b => b.Id == id);
  }

  public new async Task<List<Bill>> GetAll()
  {
    return await _context.Bills
      .Include(b => b.BillItems)
      .AsNoTracking()
      .ToListAsync();
  }
}
