using Domain.Entities;
using Domain.Repositories;

namespace Application.Commands.SubCategories;
public class CreateSubCategoryCommandHandler: ICommandHandler<SubCategory, SubCategory>
{
  private readonly ISubCategoryRepository _subCategoryrepository;

  public CreateSubCategoryCommandHandler(ISubCategoryRepository subCategoryrepository)
  {
    _subCategoryrepository = subCategoryrepository;
  }

  public async Task<ICommandResult<SubCategory>> Handle(SubCategory command)
  {
    var newSubCategory = new SubCategory(command.Name, command.CategoryId, command.Recurring);
    await _subCategoryrepository.Add(newSubCategory);

    return new CommandResult<SubCategory>(true, "SubCategory created successfully", newSubCategory);
  }
}
