using Application.Commands.Bills.Inputs;
using Domain.Entities.Bills;
using Domain.Repositories;
using Domain.Repositories.Bills;

namespace Application.Commands.Bills;
public class CreateBillCommandHandler(IBillRepository repository, IRepository<BillItem> billItemRepository) 
  : ICommandHandler<CreateUpdateBillInput, Bill>
{
  public async Task<ICommandResult<Bill>> Handle(CreateUpdateBillInput command)
  {
    var newBill = await repository.Add(new Bill(command.PaymentMonth, command.TotalIncoming));

    foreach (var billItem in command.BillItems)
      await billItemRepository.Add(new BillItem(newBill.Id, billItem.SubCategoryId, billItem.Description,
        billItem.Value));

    return new CommandResult<Bill>(true, "Bill created successfully", newBill);
  }
}
