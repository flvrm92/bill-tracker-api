namespace Application.Commands;

public class CommandResult<T> : ICommandResult<T> where T : class
{
  public CommandResult(bool success, string message, T data)
  {
    Success = success;
    Message = message;
    Data = data;
  }

  public bool Success { get; private set;  }
  public string Message { get; private set; }
  public T Data { get; private set; }
}
