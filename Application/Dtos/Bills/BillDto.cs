namespace Application.Dtos.Bills;

public class BillDto
{
  public BillDto()
  {
    BillItems = new List<BillItemDto>();
  }

  public Guid? Id { get; set; }
  public Guid? UserId { get; set; }
  public DateTime Payment { get; set; }
  public decimal Total { get; set; }
  public decimal TotalIncoming { get; set; }

  public ICollection<BillItemDto> BillItems { get; set; }
}
