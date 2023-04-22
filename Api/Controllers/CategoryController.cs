using Application.Commands.Categories;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CategoryController : Controller
  {
    private readonly IMapper _mapper;

    public CategoryController(IMapper mapper)
    {
      _mapper = mapper;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<CategoryDto>), StatusCodes.Status200OK)]
    public ActionResult<List<CategoryDto>> Get(
      [FromServices] IRepository<Category> repository)
    {
      var categories = repository.GetAll().ToList();
      var result = _mapper.Map<List<CategoryDto>>(categories);
      return Ok(result.OrderBy(x => x.Name));
    }

    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<CategoryDto>> Get(Guid id,
      [FromServices] IRepository<Category> repository)
    {
      var category = await repository.GetById(id);
      return Ok(_mapper.Map<CategoryDto>(category));
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
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
          return Ok(_mapper.Map<CategoryDto>(result.Data));

        return BadRequest(new { message = result?.Message });
      }
      catch (Exception)
      {
        return BadRequest(new { message = "There was a problem to create the category" });
      }
    }

    [HttpPut("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Category>> Put(Guid id,
      [FromBody] CategoryDto dto,
      [FromServices] CreateUpdateCategoryCommandHandler handler)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      try
      {
        if (id == Guid.Empty) return BadRequest(new { message = "Id is required" });

        dto.Id = id;
        var command = _mapper.Map<Category>(dto);
        var result = await handler.Handle(command);
        return Ok(_mapper.Map<CategoryDto>(result));
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
