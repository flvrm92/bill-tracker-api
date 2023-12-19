using Domain.Entities;

namespace Domain.Repositories;
public interface ISubCategoryRepository: IRepository<SubCategory>
{
  new Task<List<SubCategory>> GetAll();
  new Task<SubCategory> GetById(Guid id);
  Task<List<SubCategory>> GetByCategoryId(Guid id);
}
