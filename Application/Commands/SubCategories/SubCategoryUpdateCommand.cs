
namespace Application.Commands.SubCategories;

public class SubCategoryUpdateCommand
{
  public Guid Id { get; set; } = Guid.Empty;
  public string Name { get; set; }
  public Guid CategoryId { get; set; }
  public bool Recurring { get; set; }
}
