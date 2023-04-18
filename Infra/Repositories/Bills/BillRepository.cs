using Domain.Entities.Bills;
using Domain.Repositories.Bills;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories.Bills;

public class BillRepository: BaseRepository<Bill>, IBillRepository
{
  private readonly ApplicationContext _context;

  public BillRepository(ApplicationContext context) 
    : base(context)
  {
    _context = context;
  }

  public new async Task<Bill> GetById(Guid id)
  {
    return await _context.Bills
      .Include(b => b.BillItems)
      .AsNoTracking()
      .FirstOrDefaultAsync(b => b.Id == id);
  }
}
