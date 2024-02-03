using Application.Commands.SubCategories.Inputs;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Commands.SubCategories;
public class CreateSubCategoryCommandHandler(ISubCategoryRepository subCategoryrepository) : ICommandHandler<CreateUpdateSubCategoryInput, SubCategory>
{
  public async Task<ICommandResult<SubCategory>> Handle(CreateUpdateSubCategoryInput command)
  {
    var newSubCategory = new SubCategory(command.Name, command.CategoryId, command.Recurring);
    await subCategoryrepository.Add(newSubCategory);

    return new CommandResult<SubCategory>(true, "SubCategory created successfully", newSubCategory);
  }
}
