using Application.Commands.SubCategories.Inputs;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Commands.SubCategories;
public class UpdateSubCategoryCommandHandler(ISubCategoryRepository repository) : ICommandHandler<CreateUpdateSubCategoryInput, SubCategory>
{
  public async Task<ICommandResult<SubCategory>> Handle(CreateUpdateSubCategoryInput command)
  {
    if (await AlreadyExists(command))
      return new CommandResult<SubCategory>(false, "Sub Category already exists for this category", null);

    if (command.Id is null) 
      return new CommandResult<SubCategory>(false, "Id is required", null);

    var subCategory = await repository.GetById(command.Id.Value);
    if (subCategory is null) return new CommandResult<SubCategory>(false, "Sub Category not found", null);

    subCategory.Update(command.Name, command.CategoryId, command.Recurring);
    await repository.Update(subCategory);

    return new CommandResult<SubCategory>(true, "Sub Category updated successfully", subCategory);
  }

  private async Task<bool> AlreadyExists(CreateUpdateSubCategoryInput command)
  {
    var all = await repository.GetByCategoryId(command.CategoryId);
    return all.Any(x => x.Name == command.Name && x.Id != command.Id);
  }
}

