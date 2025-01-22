using Domain.Entities.Bills;

namespace Domain.Entities;

public sealed class SubCategory: BaseEntity
{
  public SubCategory(string name, Guid categoryId, bool recurring)
  {
    Name = name;
    CategoryId = categoryId;
    Recurring = recurring;

    BillItems = new List<BillItem>();
  }

  public string Name { get; private set; }
  public bool Recurring { get; private set; }
  
  public Guid CategoryId { get; private set;}
  public Category Category { get; private set; }

  public ICollection<BillItem> BillItems { get; private set; }

  public void Update(string name, Guid categoryId, bool recurring) 
    => (Name, CategoryId, Recurring) = (name, categoryId, recurring);
}

