using Application.Dtos;
using Domain.Entities.Bills;
using Domain.Repositories.Bills;

namespace Application.Commands.Bills;
public class DeleteBillCommandHandler(IBillRepository repository) : ICommandHandler<DeleteDto, Bill>
{
  public async Task<ICommandResult<Bill>> Handle(DeleteDto command)
  {
    var bill = await repository.GetById(command.Id);
    if (bill is null) return new CommandResult<Bill>(false, "Bill not found", null);
    await repository.Delete(bill);
    return new CommandResult<Bill>(true, "Bill deleted successfully", bill);
  }
}
