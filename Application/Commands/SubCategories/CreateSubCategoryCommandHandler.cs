using Application.Commands.SubCategories.Inputs;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Commands.SubCategories;
public class CreateSubCategoryCommandHandler(ISubCategoryRepository repository) : ICommandHandler<CreateUpdateSubCategoryInput, SubCategory>
{
  public async Task<ICommandResult<SubCategory>> Handle(CreateUpdateSubCategoryInput command)
  {
    if (await AlreadyExists(command))
      return new CommandResult<SubCategory>(false, "Sub Category already exists for this category", null);

    var newSubCategory = new SubCategory(command.Name, command.CategoryId, command.Recurring);
    await repository.Add(newSubCategory);

    return new CommandResult<SubCategory>(true, "SubCategory created successfully", newSubCategory);
  }

  private async Task<bool> AlreadyExists(CreateUpdateSubCategoryInput command)
  {
    var all = await repository.GetByCategoryId(command.CategoryId);
    return all.Any(x => x.Name == command.Name);
  }
}
