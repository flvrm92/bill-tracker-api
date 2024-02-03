namespace Application.Commands.Categories.Inputs;

public record CreateUpdateCategoryInput(string Name)
{
  public Guid? Id { get; private set; }
  public string Name { get; private set; } = Name;

  public void SetId(Guid id) => Id = id;
}
