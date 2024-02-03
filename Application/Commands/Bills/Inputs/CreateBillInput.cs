
namespace Application.Commands.Bills.Inputs;
public record CreateBillInput(DateOnly PaymentMonth, decimal TotalIncoming)
{
  public DateOnly PaymentMonth { get; private set; } = PaymentMonth;
  public decimal TotalIncoming { get; private set; } = TotalIncoming;
}
