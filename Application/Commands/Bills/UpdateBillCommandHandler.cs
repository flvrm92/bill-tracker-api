using Application.Commands.Bills.Inputs;
using Application.Dtos.Bills;
using Domain.Entities.Bills;
using Domain.Repositories;
using Domain.Repositories.Bills;

namespace Application.Commands.Bills;

public class UpdateBillCommandHandler(
  IBillRepository repository,
  IRepository<BillItem> billItemRepository) 
  : ICommandHandler<CreateUpdateBillInput, Bill>
{

  public async Task<ICommandResult<Bill>> Handle(CreateUpdateBillInput command)
  {
    if (command.Id is null) return new CommandResult<Bill>(false, "Id is required", null);
    var bill = await repository.GetById(command.Id.Value);

    bill.Update(bill.PaymentMonth != command.PaymentMonth ? command.PaymentMonth : bill.PaymentMonth,
      decimal.Compare(bill.TotalIncoming, command.TotalIncoming) != 0 ? command.TotalIncoming : bill.TotalIncoming);

    await UpdateItems(bill, [.. command.BillItems]);

    await repository.Update(bill);

    return new CommandResult<Bill>(true, "Bill updated successfully", bill);
  }

  private async Task UpdateItems(Bill bill, List<BillItemDto> billItemsDto)
  {
    foreach (var dto in billItemsDto)
    {
      var billItem = bill.BillItems.FirstOrDefault(x => x.Id == dto.Id);
      if (billItem is null)
      {
        var newItem = await billItemRepository.Add(new BillItem(bill.Id, dto.SubCategoryId, dto.Description, dto.Value));
        dto.SetId(newItem.Id);
      }
      else billItem.Update(dto.SubCategoryId, dto.Description, dto.Value);
    }

    var toRemove = bill.BillItems.Where(x => !billItemsDto.Select(y => y.Id).Contains(x.Id)).ToList();
    foreach (var item in toRemove) bill.BillItems.Remove(item);
  }
}
