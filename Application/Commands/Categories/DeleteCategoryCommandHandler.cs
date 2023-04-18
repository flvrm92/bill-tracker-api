using Application.Dtos;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Commands.Categories
{
  public class DeleteCategoryCommandHandler : ICommandHandler<DeleteDto, Category>
  {
    private readonly IRepository<Category> _repository;

    public DeleteCategoryCommandHandler(IRepository<Category> repository)
    {
      _repository = repository;
    }

    public async Task<ICommandResult<Category>> Handle(DeleteDto dto)
    {
      var category = await _repository.GetById(dto.Id);
      if (category is null)
        return new CommandResult<Category>(false, "Category not found", null);

      await _repository.Delete(category);

      return new CommandResult<Category>(true, "Category deleted successfully", null);
    }
  }
}
