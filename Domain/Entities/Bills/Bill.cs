namespace Domain.Entities.Bills;

public class Bill(DateOnly paymentMonth, decimal totalIncoming) : BaseEntity
{
  public DateOnly PaymentMonth { get; private set; } = paymentMonth;
  public decimal TotalIncoming { get; private set; } = totalIncoming;

  public ICollection<BillItem> BillItems { get; private set; } = new List<BillItem>();

  public void Update(DateOnly paymentMonth, decimal totalIncoming)
  {
    PaymentMonth = paymentMonth;
    TotalIncoming = totalIncoming;
  }

  public void AddBillItem(BillItem billItem) => BillItems.Add(billItem);
}