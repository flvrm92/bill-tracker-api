namespace Domain.Entities.Bills;
public class BillItem(Guid billId, Guid subCategoryId, string description, decimal value)
  : BaseEntity
{
  public Guid BillId { get; private set; } = billId;
  public virtual Bill Bill { get; private set; }

  public Guid SubCategoryId { get; private set; } = subCategoryId;
  public virtual SubCategory SubCategory { get; private set; }

  public string Description { get; private set; } = description;
  public decimal Value { get; private set; } = value;

  public void Update(Guid subCategoryId, string description, decimal value)
  {
    SubCategoryId = subCategoryId;
    Description = description;
    Value = value;
  }
}
