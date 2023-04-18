namespace Domain.Entities.Bills;

public class Bill : BaseEntity
{
  public Bill(Guid userId, DateTime payment, decimal total, decimal totalIncoming)
  {
    UserId = userId;
    Payment = payment;
    Total = total;
    TotalIncoming = totalIncoming;

    BillItems = new List<BillItem>();
  }

  public Guid UserId { get; private set; }
  public DateTime Payment { get; private set; }
  public decimal Total { get; private set; }
  public decimal TotalIncoming { get; private set; }

  public ICollection<BillItem> BillItems { get; private set; }

  public void Update(Guid userId, DateTime payment, decimal total, decimal totalIncoming)
  {
    UserId = userId;
    Payment = payment;
    Total = total;
    TotalIncoming = totalIncoming;
  }

  public void AddBillItem(BillItem billItem) => BillItems.Add(billItem);
}