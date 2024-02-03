using Application.Dtos;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Commands.Categories;
public class DeleteCategoryCommandHandler(IRepository<Category> repository) : ICommandHandler<DeleteDto, Category>
{
  public async Task<ICommandResult<Category>> Handle(DeleteDto dto)
  {
    var category = await repository.GetById(dto.Id);
    if (category is null)
      return new CommandResult<Category>(false, "Category not found", null);

    await repository.Delete(category);

    return new CommandResult<Category>(true, "Category deleted successfully", null);
  }
}
