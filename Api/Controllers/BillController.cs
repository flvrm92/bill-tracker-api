using Application.Commands.Bills;
using Application.Commands.Bills.Inputs;
using Application.Dtos.Bills;
using AutoMapper;
using Domain.Repositories.Bills;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class BillController(IMapper mapper) : Controller
  {
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<BillDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BillDto>>> Get(
      [FromServices] IBillRepository repository)
    {
      var bills = await repository.GetAll();
      var result = mapper.Map<IEnumerable<BillDto>>(bills);
      return Ok(result.OrderByDescending(x => x.PaymentMonth));
    }

    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<BillDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BillDto>>> Get(Guid id,
      [FromServices] IBillRepository repository)
    {
      var bill = await repository.GetById(id);
      var result = mapper.Map<BillDto>(bill);
      return Ok(result);
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BillDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BillDto>> Post(
      [FromBody] CreateBillInput command,
      [FromServices] CreateBillCommandHandler handler)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      try
      {
        var result = await handler.Handle(command);
        return Ok(mapper.Map<BillDto>(result.Data));
      }
      catch (Exception)
      {
        return BadRequest(new { message = "There was a problem to create the bill" });
      }
    }

    [HttpPut("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BillDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpdateBillInput>> Put(Guid id,
      [FromBody] UpdateBillInput command,
      [FromServices] UpdateBillCommandHandler handler)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      try
      {
        command.SetId(id);
        var result = await handler.Handle(command);
        return Ok(mapper.Map<BillDto>(result.Data));
      }
      catch (Exception ex)
      {
        var msg = ex.Message;
        return BadRequest(new { message = "There was a problem to update the bill" });
      }
    }

    //[HttpDelete]
    //[Produces("application/json")]
    //public async Task<ActionResult<Bill>> Delete(
    //  [FromBody] BillDto command,
    //  [FromServices] DeleteBillCommandHandler handler)
    //{
    //  if (!ModelState.IsValid)
    //    return BadRequest(ModelState);

    //  try
    //  {
    //    var result = await handler.Handle(command);
    //    return Ok(result);
    //  }
    //  catch (Exception)
    //  {
    //    return BadRequest(new { message = "There was a problem to delete the bill" });
    //  }
    //}

  }
}
