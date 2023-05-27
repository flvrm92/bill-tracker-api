using Domain.Entities;
using Domain.Repositories;

namespace Application.Commands.SubCategories;
public class UpdateSubCategoryCommandHandler : ICommandHandler<SubCategoryUpdateCommand, SubCategory>
{
  private readonly ISubCategoryRepository _subCategoryrepository;

  public UpdateSubCategoryCommandHandler(ISubCategoryRepository subCategoryrepository)
  {
    _subCategoryrepository = subCategoryrepository;
  }

  public async Task<ICommandResult<SubCategory>> Handle(SubCategoryUpdateCommand command)
  {
    var subCategory = await _subCategoryrepository.GetById(command.Id);
    if (subCategory is null) return new CommandResult<SubCategory>(false, "Sub Category not found", null);

    subCategory.Update(command.Name, command.CategoryId, command.Recurring);
    await _subCategoryrepository.Update(subCategory);

    return new CommandResult<SubCategory>(true, "Sub Category updated successfully", subCategory);
  }
}

