namespace Domain.Entities.Bills;
public sealed class BillItem(Guid billId, Guid subCategoryId, string description, decimal value)
  : BaseEntity
{
  public Guid BillId { get; private set; } = billId;
  public Bill Bill { get; private set; }

  public Guid SubCategoryId { get; private set; } = subCategoryId;
  public SubCategory SubCategory { get; private set; }

  public string Description { get; private set; } = description;
  public decimal Value { get; private set; } = value;

  public void Update(Guid subCategoryId, string description, decimal value)
  {
    SubCategoryId = subCategoryId;
    Description = description;
    Value = value;
  }
}
