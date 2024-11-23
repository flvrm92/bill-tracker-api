using Api.Controllers;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Tests.Controllers;
public class CategoryControllerTests
{
  [Fact]
  public void Should_Return_NotFound_If_Returns_Empty()
  {
    var mapperMock = new Mock<IMapper>();
    var categoryRepositoryMock = new Mock<IRepository<Category>>();

    categoryRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Category>().AsQueryable);

    var controller = new CategoryController(mapperMock.Object)
    {
      ControllerContext = new ControllerContext
      {
        HttpContext = new DefaultHttpContext()
      }
    };

    var result = controller.Get(categoryRepositoryMock.Object);
    var notFound = result.Result as NotFoundResult;
    
    Assert.Equal(404, notFound?.StatusCode);
  }

  [Fact]
  public void Should_Return_CategoryDto_List_And_Ok()
  {
    var mapperMock = new Mock<IMapper>();
    var categoryRepositoryMock = new Mock<IRepository<Category>>();

    var categoryList = new List<Category> { new("Test 1"), new("Test 2") };

    var categoryDtoList = new List<CategoryDto> { new() { Name = "Test 1" }, new() { Name = "Test 2" } };

    categoryRepositoryMock.Setup(x => x.GetAll()).Returns(categoryList.AsQueryable);
    mapperMock.Setup(x => x.Map<IReadOnlyCollection<CategoryDto>>(It.IsAny<List<Category>>())).Returns(categoryDtoList);

    var controller = new CategoryController(mapperMock.Object)
    {
      ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
    };

    var result = controller.Get(categoryRepositoryMock.Object);
    var okResult = result.Result as OkObjectResult;

    Assert.Equal(200, okResult?.StatusCode);
    Assert.Equal(categoryDtoList, okResult?.Value);
  }
}
