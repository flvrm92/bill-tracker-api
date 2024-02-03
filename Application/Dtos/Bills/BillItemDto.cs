
namespace Application.Dtos.Bills;
public record BillItemDto
{
  public Guid? Id { get; set; }
  public Guid SubCategoryId { get; set; }
  public string Description { get; set; }
  public decimal Value { get; set; }
}
