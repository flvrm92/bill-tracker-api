
namespace Application.Dtos;
public class SubCategoryDto
{
  public Guid? Id { get; set; }
  public string Name { get; set; }
  public bool Recurring { get; set; }

  public CategoryDto Category { get; set; }
}
