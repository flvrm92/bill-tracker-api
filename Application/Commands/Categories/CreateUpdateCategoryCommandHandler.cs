using Domain.Entities;
using Domain.Repositories;

namespace Application.Commands.Categories;
public class CreateUpdateCategoryCommandHandler: ICommandHandler<Category, Category>
{
  private readonly IRepository<Category> _repository;

  public CreateUpdateCategoryCommandHandler(IRepository<Category> repository)
  {
    _repository = repository;
  }

  public async Task<ICommandResult<Category>> Handle(Category command)
  {
    var category = await _repository.GetById(command.Id);
    if (category is not null)
    {
      category.Update(command.Name);
      await _repository.Update(category);

      return new CommandResult<Category>(true, "Category updated successfully", category);
    }

    var newCategory = new Category(command.Name);
    await _repository.Add(newCategory);

    return new CommandResult<Category>(true, "Category created successfully", newCategory);
  }
}