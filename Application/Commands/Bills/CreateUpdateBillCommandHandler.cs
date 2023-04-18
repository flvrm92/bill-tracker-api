using Application.Dtos.Bills;
using Domain.Entities.Bills;
using Domain.Repositories;

namespace Application.Commands.Bills;
public class CreateUpdateBillCommandHandler : ICommandHandler<BillDto, Bill>
{
  private readonly IRepository<Bill> _repository;

  public CreateUpdateBillCommandHandler(IRepository<Bill> repository)
  {
    _repository = repository;
  }

  public async Task<ICommandResult<Bill>> Handle(BillDto command)
  {
    var bill = await _repository.GetById(command.Id ?? Guid.NewGuid());
    if (bill is not null)
    {
      bill.Update(command.UserId ?? Guid.NewGuid(), command.Payment, command.Total, command.TotalIncoming);
      bill.BillItems.Clear();

      command.BillItems
        .ToList()
        .ForEach(item =>
          bill.AddBillItem(new BillItem(bill.Id, item.CategoryId, item.Description, item.Value)));

      await _repository.Update(bill);

      return new CommandResult<Bill>(true, "Bill updated successfully", bill);
    }

    var newBill = new Bill(command.UserId ?? Guid.NewGuid(), command.Payment, command.Total, command.TotalIncoming);
    command.BillItems
      .ToList()
      .ForEach(item =>
        newBill.AddBillItem(new BillItem(newBill.Id, item.CategoryId, item.Description, item.Value)));

    await _repository.Add(newBill);

    return new CommandResult<Bill>(true, "Bill created successfully", newBill);
  }
}
