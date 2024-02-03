using Application.Commands.SubCategories.Inputs;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Commands.SubCategories;
public class UpdateSubCategoryCommandHandler(ISubCategoryRepository subCategoryrepository) : ICommandHandler<CreateUpdateSubCategoryInput, SubCategory>
{
  public async Task<ICommandResult<SubCategory>> Handle(CreateUpdateSubCategoryInput command)
  {
    if (command.Id is null) return new CommandResult<SubCategory>(false, "Id is required", null);

    var subCategory = await subCategoryrepository.GetById(command.Id.Value);
    if (subCategory is null) return new CommandResult<SubCategory>(false, "Sub Category not found", null);

    subCategory.Update(command.Name, command.CategoryId, command.Recurring);
    await subCategoryrepository.Update(subCategory);

    return new CommandResult<SubCategory>(true, "Sub Category updated successfully", subCategory);
  }
}

