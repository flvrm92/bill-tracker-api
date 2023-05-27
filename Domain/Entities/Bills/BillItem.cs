namespace Domain.Entities.Bills;
public class BillItem : BaseEntity
{
    public BillItem(Guid billId, Guid subCategoryId, string description, decimal value)
    {
        BillId = billId;
        SubCategoryId = subCategoryId;
        Description = description;
        Value = value;
    }

    public Guid BillId { get; private set; }
    public virtual Bill Bill { get; private set; }

    public Guid SubCategoryId { get; private set; }
    public virtual SubCategory SubCategory { get; private set; }

    public string Description { get; private set; }
    public decimal Value { get; private set; }

    public void Update(Guid billId, Guid subCategoryId, string description, decimal value)
    {
        BillId = billId;
        SubCategoryId = subCategoryId;
        Description = description;
        Value = value;
    }
}
