
namespace Application.Dtos.Bills;
public class BillItemDto
{
  public Guid? Id { get; set; }
  public Guid CategoryId { get; set; }
  public string Description { get; set; }
  public decimal Value { get; set; }
}
