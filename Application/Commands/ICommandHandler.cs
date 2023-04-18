
namespace Application.Commands; 
public interface ICommandHandler<in T1, T2>
  where T1: class
  where T2: class
{
  Task<ICommandResult<T2>> Handle(T1 command);
}
