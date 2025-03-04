﻿using Application.Commands.Categories;
using Application.Commands.Categories.Inputs;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CategoryController(IMapper mapper) : Controller
  {
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IReadOnlyCollection<CategoryDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyCollection<CategoryDto>> Get(
      [FromServices] IRepository<Category> repository)
    {
      var result = mapper.Map<IReadOnlyCollection<CategoryDto>>(repository.GetAll().ToList());
      if (result is null || result.Count <= 0) return NotFound();
      return Ok(result.OrderBy(x => x.Name));
    }

    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<CategoryDto>> Get(Guid id,
      [FromServices] IRepository<Category> repository)
    {
      var category = await repository.GetById(id);
      return Ok(mapper.Map<CategoryDto>(category));
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoryDto>> Post(
      [FromBody] CreateUpdateCategoryInput command,
      [FromServices] CreateUpdateCategoryCommandHandler handler)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      try
      {
        var result = await handler.Handle(command);
        if (result is not null && result.Success)
          return Ok(mapper.Map<CategoryDto>(result.Data));

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
      [FromBody] CreateUpdateCategoryInput command,
      [FromServices] CreateUpdateCategoryCommandHandler handler)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      try
      {
        if (id == Guid.Empty) return BadRequest(new { message = "Id is required" });

        command.SetId(id);
        var result = await handler.Handle(command);
        if (result is not null && result.Success) 
          return Ok(mapper.Map<CategoryDto>(result.Data));

        return BadRequest(new { message = result?.Message });
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
        var result = await handler.Handle(new DeleteDto(id));
        if (result is not null && result.Success) return Ok(result);

        return BadRequest(new { message = result?.Message });
      }
      catch (Exception)
      {
        return BadRequest(new { message = "There was a problem to delete the category" });
      }
    }
  }
}
