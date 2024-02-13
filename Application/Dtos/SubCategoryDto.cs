
namespace Application.Dtos;
public record SubCategoryDto
{
  public Guid? Id { get; set; }
  public string Name { get; set; }
  public bool Recurring { get; set; }
  public Guid CategoryId { get; set; }

  public CategoryDto Category { get; set; }
}
