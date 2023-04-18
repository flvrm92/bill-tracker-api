namespace Domain.Entities.Bills;
public class BillItem : BaseEntity
{
    public BillItem(Guid billId, Guid categoryId, string description, decimal value)
    {
        BillId = billId;
        CategoryId = categoryId;
        Description = description;
        Value = value;
    }

    public Guid BillId { get; private set; }
    public virtual Bill Bill { get; private set; }

    public Guid CategoryId { get; private set; }
    public virtual Category Category { get; private set; }

    public string Description { get; private set; }
    public decimal Value { get; private set; }

    public void Update(Guid billId, Guid categoryId, string description, decimal value)
    {
        BillId = billId;
        CategoryId = categoryId;
        Description = description;
        Value = value;
    }
}
