using Application.Commands.SubCategories;
using Application.Commands.SubCategories.Inputs;
using Domain.Entities;
using Domain.Repositories;
using Xunit;
using NSubstitute;

namespace Tests.Commands.SubCategories;
public class CreateSubCategoryCommandHandlerTests
{
  [Fact]
  public async Task Handle_ShouldCreateSubCategory()
  {
    // Arrange
    var categoryId = Guid.NewGuid();
    var repository = Substitute.For<ISubCategoryRepository>();
    var subCategory = new SubCategory("Test SubCategory", categoryId, true);
    var command = new CreateUpdateSubCategoryInput("Test SubCategory", categoryId, true);
    var handler = new CreateSubCategoryCommandHandler(repository);

    repository.GetByCategoryId(Arg.Any<Guid>()).Returns([]);

    // Act
    var result = await handler.Handle(command);

    // Assert
    await repository.Received().Add(Arg.Any<SubCategory>());
    Assert.NotNull(result);
    Assert.Equal(subCategory.Name, result.Data.Name);
    Assert.Equal(subCategory.CategoryId, result.Data.CategoryId);
  }

  [Fact]
  public async Task Handle_Should_Return_Error_If_Exists()
  {
    // Arrange
    var categoryId = Guid.NewGuid();
    var repository = Substitute.For<ISubCategoryRepository>();
    var command = new CreateUpdateSubCategoryInput("Test SubCategory", categoryId, true);
    var handler = new CreateSubCategoryCommandHandler(repository);

    var existentSubCategory = new SubCategory("Test SubCategory", categoryId, true);

    repository.GetByCategoryId(Arg.Any<Guid>()).Returns([existentSubCategory]);

    // Act
    var result = await handler.Handle(command);

    // Assert

    await repository.DidNotReceive().Add(Arg.Any<SubCategory>());
    Assert.NotNull(result);
    Assert.Equal("Sub Category already exists for this category", result.Message);
    Assert.False(result.Success);
  }
}
