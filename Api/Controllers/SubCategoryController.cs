using Application.Commands.SubCategories;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class SubCategoryController : Controller
  {
    private readonly IMapper _mapper;
    public SubCategoryController(IMapper mapper)
    {
      _mapper = mapper;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<SubCategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<SubCategoryDto>>> Get(
      [FromServices] ISubCategoryRepository repository)
    {
      var subCategories = await repository.GetAll();
      var result = _mapper.Map<List<SubCategoryDto>>(subCategories);
      return Ok(result.OrderBy(x => x.Name));
    }

    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(SubCategory), StatusCodes.Status200OK)]
    public async Task<ActionResult<SubCategory>> Get(Guid id,
      [FromServices] ISubCategoryRepository repository)
    {
      return Ok(await repository.GetById(id));
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(SubCategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SubCategory>> Post(
      [FromBody] SubCategory command,
      [FromServices] CreateSubCategoryCommandHandler handler)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      try
      {
        var result = await handler.Handle(command);
        if (result is not null && result.Success)
          return Ok(_mapper.Map<SubCategoryDto>(result.Data));

        return BadRequest(new { message = result?.Message });
      }
      catch (Exception)
      {
        return BadRequest(new { message = "There was a problem to create the subcategory" });
      }
    }

    [HttpPut("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(SubCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Category>> Put(Guid id,
      [FromBody] SubCategoryUpdateCommand command,
      [FromServices] UpdateSubCategoryCommandHandler handler)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      try
      {
        if (id == Guid.Empty) return BadRequest(new { message = "Id is required" });

        command.Id = id;
        var result = await handler.Handle(command);
        return Ok(_mapper.Map<SubCategoryDto>(result.Data));
      }
      catch (Exception)
      {
        return BadRequest(new { message = "There was a problem to update the subcategory" });
      }
    }

    [HttpDelete("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SubCategory>> Delete(
      [FromRoute] Guid id,
      [FromServices] DeleteSubCategoryCommandHandler handler)
    {
      try
      {
        var result = await handler.Handle(new DeleteDto { Id = id });
        return Ok(result);
      }
      catch (Exception)
      {
        return BadRequest(new { message = "There was a problem to delete the subcategory" });
      }
    }

  }
}
