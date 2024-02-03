namespace Application.Commands.SubCategories.Inputs;

public record CreateUpdateSubCategoryInput(string Name, Guid CategoryId, bool Recurring)
{
    public Guid? Id { get; private set; }
    public string Name { get; private set; } = Name;
    public Guid CategoryId { get; private set; } = CategoryId;
    public bool Recurring { get; private set; } = Recurring;

    public void SetId(Guid id) => Id = id;
}
