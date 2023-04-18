using Application.Commands.Categories;
using Application.Dtos;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CategoryController : Controller
  {
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<Category>), StatusCodes.Status200OK)]
    public ActionResult<List<Category>> Get(
      [FromServices] IRepository<Category> repository)
    {
      var categories = repository.GetAll().ToList();
      return Ok(categories.OrderBy(s => s.Name));
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Category>> Post(
      [FromBody] Category command,
      [FromServices] CreateUpdateCategoryCommandHandler handler)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      try
      {
        var result = await handler.Handle(command);
        if (result is not null && result.Success)
          return Ok(result.Data);

        return BadRequest(new { message = result?.Message });
      }
      catch (Exception)
      {
        return BadRequest(new { message = "There was a problem to create the category" });
      }
    }

    [HttpPut]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Category>> Put(
      [FromBody] Category command,
      [FromServices] CreateUpdateCategoryCommandHandler handler)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      try
      {
        var result = await handler.Handle(command);
        return Ok(result);
      }
      catch (Exception)
      {
        return BadRequest(new { message = "There was a problem to update the category" });
      }
    }

    [HttpDelete("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Category>> Delete(
      [FromRoute] Guid id,
      [FromServices] DeleteCategoryCommandHandler handler)
    {
      try
      {
        var result = await handler.Handle(new DeleteDto { Id = id });
        return Ok(result);
      }
      catch (Exception)
      {
        return BadRequest(new { message = "There was a problem to delete the category" });
      }
    }
  }
}
