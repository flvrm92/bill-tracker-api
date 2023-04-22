using Application.Commands.Bills;
using Application.Dtos.Bills;
using AutoMapper;
using Domain.Entities.Bills;
using Domain.Repositories.Bills;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class BillController : Controller
  {
    private readonly IMapper _mapper;
    public BillController(IMapper mapper)
    {
      _mapper = mapper;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<BillDto>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<BillDto>> Get(
      [FromServices] IBillRepository repository)
    {
      var bills = repository.GetAll().ToList();
      var result = _mapper.Map<IEnumerable<BillDto>>(bills);
      return Ok(result.OrderByDescending(x => x.Payment));
    }

    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<BillDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BillDto>>> Get(Guid id,
      [FromServices] IBillRepository repository)
    {
      var bill = await repository.GetById(id);
      var result = _mapper.Map<BillDto>(bill);
      return Ok(result);
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BillDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BillDto>> Post(
      [FromBody] BillDto command,
      [FromServices] CreateUpdateBillCommandHandler handler)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      try
      {
        var result = await handler.Handle(command);
        return Ok(_mapper.Map<BillDto>(result.Data));
      }
      catch (Exception)
      {
        return BadRequest(new { message = "There was a problem to create the bill" });
      }
    }

    [HttpPut]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BillDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BillDto>> Put(
      [FromBody] BillDto command,
      [FromServices] CreateUpdateBillCommandHandler handler)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      try
      {
        var result = await handler.Handle(command);
        return Ok(_mapper.Map<BillDto>(result.Data));
      }
      catch (Exception)
      {
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
