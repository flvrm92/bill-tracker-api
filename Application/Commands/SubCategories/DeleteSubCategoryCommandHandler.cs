using Application.Dtos;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Commands.SubCategories
{
  public class DeleteSubCategoryCommandHandler: ICommandHandler<DeleteDto, SubCategory>
  {
    private readonly ISubCategoryRepository _subCategoryRepository;
    
    public DeleteSubCategoryCommandHandler(ISubCategoryRepository subCategoryRepository)
    {
      _subCategoryRepository = subCategoryRepository;
    }

    public async Task<ICommandResult<SubCategory>> Handle(DeleteDto dto)
    {
      var subCategory = await _subCategoryRepository.GetById(dto.Id);
      if (subCategory is null)
        return new CommandResult<SubCategory>(false, "SubCategory not found", null);

      await _subCategoryRepository.Delete(subCategory);

      return new CommandResult<SubCategory>(true, "SubCategory deleted successfully", null);
    }
  }
}
