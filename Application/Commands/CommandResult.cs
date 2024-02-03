namespace Application.Commands;

public class CommandResult<T>(bool success, string message, T data) 
  : ICommandResult<T> where T 
  : class
{
  public bool Success { get; private set; } = success;
  public string Message { get; private set; } = message;
  public T Data { get; private set; } = data;
}
