
namespace Application.Dtos.Bills;

public record BillItemDto(Guid? Id, Guid SubCategoryId, string Description, decimal Value)
{
  public Guid? Id { get; private set; } = Id;
  public Guid SubCategoryId { get; private set; } = SubCategoryId;
  public string Description { get; private set; } = Description;
  public decimal Value { get; private set; } = Value;

  public void SetId(Guid id) => Id = id;
}
