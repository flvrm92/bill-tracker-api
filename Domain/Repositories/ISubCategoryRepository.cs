using Domain.Entities;

namespace Domain.Repositories;
public interface ISubCategoryRepository: IRepository<SubCategory>
{
  new Task<List<SubCategory>> GetAll();
  Task<SubCategory> GetById(Guid id, CancellationToken cancellationToken);
  Task<List<SubCategory>> GetByCategoryId(Guid id);
}
