using Domain.Entities.Bills;

namespace Domain.Repositories.Bills;
public interface IBillRepository: IRepository<Bill>
{
  new Task<Bill> GetById(Guid id);
}
