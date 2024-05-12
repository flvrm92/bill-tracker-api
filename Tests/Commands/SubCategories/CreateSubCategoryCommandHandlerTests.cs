using Application.Commands.SubCategories;
using Application.Commands.SubCategories.Inputs;
using Domain.Entities;
using Domain.Repositories;
using Moq;
using Xunit;

namespace Tests.Commands.SubCategories;
public class CreateSubCategoryCommandHandlerTests
{
  [Fact]
  public async Task Handle_ShouldCreateSubCategory()
  {
    // Arrange
    var categoryId = Guid.NewGuid();
    var repository = new Mock<ISubCategoryRepository>();
    var subCategory = new SubCategory("Test SubCategory", categoryId, true);
    var command = new CreateUpdateSubCategoryInput("Test SubCategory", categoryId, true);
    var handler = new CreateSubCategoryCommandHandler(repository.Object);

    repository
      .Setup(x => x.GetByCategoryId(It.IsAny<Guid>()))
      .ReturnsAsync([]);

    // Act
    var result = await handler.Handle(command);

    // Assert
    repository.Verify(x => x.Add(It.IsAny<SubCategory>()), Times.Once);
    Assert.NotNull(result);
    Assert.Equal(subCategory.Name, result.Data.Name);
    Assert.Equal(subCategory.CategoryId, result.Data.CategoryId);
  }

  [Fact]
  public async Task Handle_Should_Return_Error_If_Exists()
  {
    // Arrange
    var categoryId = Guid.NewGuid();
    var repository = new Mock<ISubCategoryRepository>();
    var command = new CreateUpdateSubCategoryInput("Test SubCategory", categoryId, true);
    var handler = new CreateSubCategoryCommandHandler(repository.Object);

    var existentSubCategory = new SubCategory("Test SubCategory", categoryId, true);

    repository
      .Setup(x => x.GetByCategoryId(It.IsAny<Guid>()))
      .ReturnsAsync([existentSubCategory]);

    // Act
    var result = await handler.Handle(command);

    // Assert
    repository.Verify(x => x.Add(It.IsAny<SubCategory>()), Times.Never);
    Assert.NotNull(result);
    Assert.Equal("Sub Category already exists for this category", result.Message);
    Assert.False(result.Success);
  }
}
