using Application.Commands.SubCategories;
using Application.Commands.SubCategories.Inputs;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
  [Route("[controller]")]
  public class SubCategoryController(IMapper mapper) : Controller
  {
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<SubCategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<SubCategoryDto>>> Get(
      [FromServices] ISubCategoryRepository repository)
    {
      var subCategories = await repository.GetAll();
      var result = mapper.Map<List<SubCategoryDto>>(subCategories);
      return Ok(result.OrderBy(x => x.Name));
    }

    [HttpGet]
    [Route("GetByCategoryId/{categoryId}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<SubCategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<SubCategoryDto>>> GetByCategoryId(Guid categoryId,
      [FromServices] ISubCategoryRepository repository)
    {
      var subCategories = await repository.GetByCategoryId(categoryId);
      var result = mapper.Map<List<SubCategoryDto>>(subCategories);
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
      [FromBody] CreateUpdateSubCategoryInput command,
      [FromServices] CreateSubCategoryCommandHandler handler)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      try
      {
        var result = await handler.Handle(command);
        if (result is not null && result.Success)
          return Ok(mapper.Map<SubCategoryDto>(result.Data));

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
      [FromBody] CreateUpdateSubCategoryInput command,
      [FromServices] UpdateSubCategoryCommandHandler handler)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      try
      {
        if (id == Guid.Empty) return BadRequest(new { message = "Id is required" });

        command.SetId(id);
        var result = await handler.Handle(command);
        return Ok(mapper.Map<SubCategoryDto>(result.Data));
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
        var result = await handler.Handle(new DeleteDto(id));
        return Ok(result);
      }
      catch (Exception)
      {
        return BadRequest(new { message = "There was a problem to delete the subcategory" });
      }
    }

  }
}
