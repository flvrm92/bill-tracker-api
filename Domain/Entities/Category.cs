using Domain.Entities.Bills;

namespace Domain.Entities;
public class Category: BaseEntity
{
  public Category (string name)
  {
    Name = name;

    BillItems = new List<BillItem>();
  }

  public string Name { get; private set; }

  public virtual ICollection<BillItem> BillItems { get; private set; }

  public void Update(string name) => Name = name;
}
