namespace Application.Dtos.Bills;

public record BillDto
{
  public Guid? Id { get; set; }
  public DateOnly PaymentMonth { get; set; }
  public decimal TotalIncoming { get; set; }

  public ICollection<BillItemDto> BillItems { get; set; } = new List<BillItemDto>();
}
