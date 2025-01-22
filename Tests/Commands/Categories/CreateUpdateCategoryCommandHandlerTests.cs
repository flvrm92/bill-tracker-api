using Application.Commands.Categories;
using Application.Commands.Categories.Inputs;
using Domain.Entities;
using Domain.Repositories;
using NSubstitute;
using Xunit;

namespace Tests.Commands.Categories;
public class CreateUpdateCategoryCommandHandlerTests
{
  [Fact]
  public async Task Handle_WhenIdIsNull_ShouldCreateCategory()
  {
    // Arrange
    var repository = Substitute.For<IRepository<Category>>();
    var handler = new CreateUpdateCategoryCommandHandler(repository);
    
    var command = new CreateUpdateCategoryInput("Category 1");
    var category = new Category("Category 1");
    repository.Add(Arg.Any<Category>()).Returns(category);
    
    // Act
    var result = await handler.Handle(command);

    // Assert

    await repository.Received().Add(Arg.Any<Category>());
    await repository.DidNotReceive().Update(Arg.Any<Category>());    
    Assert.True(result.Success);
    Assert.Equal("Category created successfully", result.Message);
    Assert.Equal(category.Name, result.Data.Name);
  }

  [Fact]
  public async Task Handle_WhenIdIsNotNull_ShouldUpdateCategory()
  {
    // Arrange
    var repository = Substitute.For<IRepository<Category>>();
    var handler = new CreateUpdateCategoryCommandHandler(repository);
    
    var command = new CreateUpdateCategoryInput("Category 2");
    var category = new Category("Category 2");
    repository.GetById(Arg.Any<Guid>()).Returns(category);
    command.SetId(category.Id);

    // Act
    var result = await handler.Handle(command);

    // Assert
    await repository.DidNotReceive().Add(Arg.Any<Category>());
    await repository.Received().Update(Arg.Any<Category>());

    Assert.True(result.Success);
    Assert.Equal("Category updated successfully", result.Message);
    Assert.Equal(category.Name, result.Data.Name);
  }

  [Fact]
  public async Task Handle_WhenIdIsNotNullAndCategoryNotFound_ShouldReturnError()
  {
    // Arrange
    var repository = Substitute.For<IRepository<Category>>();
    var handler = new CreateUpdateCategoryCommandHandler(repository);
    
    var command = new CreateUpdateCategoryInput("Category 3");
    var category = new Category("Category 3");
    repository.GetById(Arg.Any<Guid>()).Returns((Category)null);
    command.SetId(category.Id);

    // Act
    var result = await handler.Handle(command);

    // Assert
    await repository.DidNotReceive().Add(Arg.Any<Category>());
    await repository.DidNotReceive().Update(Arg.Any<Category>());
    Assert.False(result.Success);
    Assert.Equal("Category not found", result.Message);
    Assert.Null(result.Data);
  }
}
