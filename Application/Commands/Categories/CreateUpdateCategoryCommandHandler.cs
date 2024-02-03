using Application.Commands.Categories.Inputs;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Commands.Categories;
public class CreateUpdateCategoryCommandHandler(IRepository<Category> repository) : ICommandHandler<CreateUpdateCategoryInput, Category>
{
  public async Task<ICommandResult<Category>> Handle(CreateUpdateCategoryInput command)
  {
    if (command.Id is not null)
    {
      var category = await repository.GetById(command.Id.Value);
      if (category is null)
        return new CommandResult<Category>(false, "Category not found", null);
      
      category.Update(command.Name);
      await repository.Update(category);

      return new CommandResult<Category>(true, "Category updated successfully", category);
    }

    var newCategory = new Category(command.Name);
    await repository.Add(newCategory);

    return new CommandResult<Category>(true, "Category created successfully", newCategory);
  }
}