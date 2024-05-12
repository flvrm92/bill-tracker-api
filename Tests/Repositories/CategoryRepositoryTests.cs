using Domain.Entities;
using Infra.Repositories;
using Xunit;

namespace Tests.Repositories;
public class CategoryRepositoryTests: TestContext
{
  [Fact]
  public async Task Should_Return_By_Id()
  {
    // Assert
    await EnsureCreated();
    var input = new Category("CategoryTest");
    
    await CreateCategory(input);

    // Arrange

    var repository = new BaseRepository<Category>(TestAppContext);
    var category = await repository.GetById(input.Id);

    // Act
    Assert.NotNull(category);
    Assert.Equal(input.Id, category.Id);
  }

  [Fact]
  public async Task Should_Return_Category_List()
  {
    // Assert
    await EnsureCreated();

    await CreateCategory(new Category("CategoryTest"));
    await CreateCategory(new Category("CategoryTest 2"));
    await CreateCategory(new Category("CategoryTest 3"));

    // Arrange
    var repository = new BaseRepository<Category>(TestAppContext);
    var category = repository.GetAll();

    // Act
    Assert.NotNull(category);
    Assert.Equal(3, category.Count());
  }
}
