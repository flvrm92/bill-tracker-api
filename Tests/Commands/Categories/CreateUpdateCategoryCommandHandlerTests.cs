using Application.Commands.Categories;
using Application.Commands.Categories.Inputs;
using Domain.Entities;
using Domain.Repositories;
using Moq;
using Xunit;

namespace Tests.Commands.Categories;
public class CreateUpdateCategoryCommandHandlerTests
{
  [Fact]
  public async Task Handle_WhenIdIsNull_ShouldCreateCategory()
  {
    // Arrange
    var repository = new Mock<IRepository<Category>>();
    var handler = new CreateUpdateCategoryCommandHandler(repository.Object);
    
    var command = new CreateUpdateCategoryInput("Category 1");
    var category = new Category("Category 1");
    repository.Setup(x => x.Add(It.IsAny<Category>())).ReturnsAsync(category);
    
    // Act
    var result = await handler.Handle(command);
    
    // Assert
    repository.Verify(x => x.Add(It.IsAny<Category>()), Times.Once);
    repository.Verify(x => x.Update(It.IsAny<Category>()), Times.Never);
    Assert.True(result.Success);
    Assert.Equal("Category created successfully", result.Message);
    Assert.Equal(category.Name, result.Data.Name);
  }

  [Fact]
  public async Task Handle_WhenIdIsNotNull_ShouldUpdateCategory()
  {
    // Arrange
    var repository = new Mock<IRepository<Category>>();
    var handler = new CreateUpdateCategoryCommandHandler(repository.Object);
    
    var command = new CreateUpdateCategoryInput("Category 2");
    var category = new Category("Category 2");
    repository.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(category);
    command.SetId(category.Id);

    // Act
    var result = await handler.Handle(command);
    
    // Assert
    repository.Verify(x => x.Add(It.IsAny<Category>()), Times.Never);
    repository.Verify(x => x.Update(It.IsAny<Category>()), Times.Once);
    Assert.True(result.Success);
    Assert.Equal("Category updated successfully", result.Message);
    Assert.Equal(category.Name, result.Data.Name);
  }

  [Fact]
  public async Task Handle_WhenIdIsNotNullAndCategoryNotFound_ShouldReturnError()
  {
    // Arrange
    var repository = new Mock<IRepository<Category>>();
    var handler = new CreateUpdateCategoryCommandHandler(repository.Object);
    
    var command = new CreateUpdateCategoryInput("Category 3");
    var category = new Category("Category 3");
    repository.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync((Category)null);
    command.SetId(category.Id);

    // Act
    var result = await handler.Handle(command);
    
    // Assert
    repository.Verify(x => x.Add(It.IsAny<Category>()), Times.Never);
    repository.Verify(x => x.Update(It.IsAny<Category>()), Times.Never);
    Assert.False(result.Success);
    Assert.Equal("Category not found", result.Message);
    Assert.Null(result.Data);
  }
}
