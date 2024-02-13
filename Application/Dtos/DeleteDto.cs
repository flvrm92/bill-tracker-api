namespace Application.Dtos;
public record DeleteDto (Guid Id)
{
  public Guid Id { get; } = Id;
}
