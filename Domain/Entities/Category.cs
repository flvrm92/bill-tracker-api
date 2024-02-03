
namespace Domain.Entities;
public class Category(string name) : BaseEntity
{
  public string Name { get; private set; } = name;

  public void Update(string name) => Name = name;

  public ICollection<SubCategory> SubCategories { get; private set; } = new List<SubCategory>();
}
