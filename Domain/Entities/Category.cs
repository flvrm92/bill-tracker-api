
namespace Domain.Entities;
public class Category: BaseEntity
{
  public Category(string name)
  {
    Name = name;

    SubCategories = new List<SubCategory>();
  }

  public string Name { get; private set; }

  public void Update(string name) => Name = name;

  public ICollection<SubCategory> SubCategories { get; private set; }

}
