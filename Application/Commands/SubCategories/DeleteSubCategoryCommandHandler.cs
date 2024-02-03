using Application.Dtos;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Commands.SubCategories;
public class DeleteSubCategoryCommandHandler(ISubCategoryRepository subCategoryRepository) : ICommandHandler<DeleteDto, SubCategory>
{
  public async Task<ICommandResult<SubCategory>> Handle(DeleteDto dto)
  {
    var subCategory = await subCategoryRepository.GetById(dto.Id);
    if (subCategory is null)
      return new CommandResult<SubCategory>(false, "SubCategory not found", null);

    await subCategoryRepository.Delete(subCategory);

    return new CommandResult<SubCategory>(true, "SubCategory deleted successfully", null);
  }
}
