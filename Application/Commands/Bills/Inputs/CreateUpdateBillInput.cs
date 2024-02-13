
using Application.Dtos.Bills;

namespace Application.Commands.Bills.Inputs;
public record CreateUpdateBillInput(
  DateOnly PaymentMonth,
  decimal TotalIncoming,
  List<BillItemDto> BillItems)
{
  public Guid? Id { get; private set; }

  public DateOnly PaymentMonth { get; private set; } = PaymentMonth;
  public decimal TotalIncoming { get; private set; } = TotalIncoming;

  public List<BillItemDto> BillItems { get; private set; } = BillItems;

  public void SetId(Guid id) => Id = id;
}
