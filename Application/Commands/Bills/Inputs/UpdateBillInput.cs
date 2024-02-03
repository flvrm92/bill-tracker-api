
using Application.Dtos.Bills;

namespace Application.Commands.Bills.Inputs;
public record UpdateBillInput(DateOnly PaymentMonth, decimal TotalIncoming, List<BillItemDto> Items)
{
  public Guid? Id { get; private set; }

  public DateOnly PaymentMonth { get; private set; } = PaymentMonth;
  public decimal TotalIncoming { get; private set; } = TotalIncoming;

  public ICollection<BillItemDto> BillItems { get; private set; } = Items;

  public void SetId(Guid id) => Id = id;
}
