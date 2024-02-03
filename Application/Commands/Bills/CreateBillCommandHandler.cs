using Application.Commands.Bills.Inputs;
using Domain.Entities.Bills;
using Domain.Repositories;

namespace Application.Commands.Bills;
public class CreateBillCommandHandler(IRepository<Bill> repository) : ICommandHandler<CreateBillInput, Bill>
{
  public async Task<ICommandResult<Bill>> Handle(CreateBillInput command)
  {
    var newBill = await repository.Add(new Bill(command.PaymentMonth, command.TotalIncoming));

    return new CommandResult<Bill>(true, "Bill created successfully", newBill);
  }
}
