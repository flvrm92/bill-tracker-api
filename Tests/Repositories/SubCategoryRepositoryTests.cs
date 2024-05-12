using Domain.Entities;
using Infra.Repositories;
using Xunit;

namespace Tests.Repositories;
public class SubCategoryRepositoryTests: TestContext
{
  [Fact]
  public async Task Should_Return_ById()
  {
    // Assert
    await EnsureCreated();
    var category = new Category("CategoryTest");
    var input = new SubCategory("SubCategory Test", category.Id, true);

    await CreateCategory(category);
    await CreateSubCategory(input);

    // Arrange
    var repository = new SubCategoryRepository(TestAppContext);
    var subCategory = await repository.GetById(input.Id);

    // Act
    Assert.NotNull(subCategory);
    Assert.Equal(input.Id, subCategory.Id);
    Assert.Equal(input.Name, subCategory.Name);
    Assert.Equal(category.Id, subCategory.CategoryId);
  }

  [Fact]
  public async Task Should_Return_SubCategory_List()
  {
    // Assert
    await EnsureCreated();
    var category = new Category("CategoryTest");
    var input1 = new SubCategory("SubCategory 1", category.Id, true);
    var input2 = new SubCategory("SubCategory 2", category.Id, false);
    var input3 = new SubCategory("SubCategory 3", category.Id, true);

    await CreateCategory(category);
    await CreateSubCategory(input1);
    await CreateSubCategory(input2);
    await CreateSubCategory(input3);

    // Arrange
    var repository = new SubCategoryRepository(TestAppContext);
    var subCategories = await repository.GetAll();

    // Act
    Assert.NotNull(subCategories);
    Assert.Equal(3, subCategories.Count);
    Assert.False(subCategories[1].Recurring);
  }

  [Fact]
  public async Task Should_Return_By_CategoryId()
  {
    // Assert
    await EnsureCreated();
    var category1 = new Category("CategoryTest");
    var category2 = new Category("New Category Test");
    var input1 = new SubCategory("SubCategory 1", category1.Id, true);
    var input2 = new SubCategory("SubCategory 2", category1.Id, false);
    var input3 = new SubCategory("SubCategory 3", category1.Id, true);
    var input4 = new SubCategory("SubCategory 1 from category 2", category2.Id, true);

    await CreateCategory(category1);
    await CreateCategory(category2);
    await CreateSubCategory(input1);
    await CreateSubCategory(input2);
    await CreateSubCategory(input3);
    await CreateSubCategory(input4);

    // Arrange
    var repository = new SubCategoryRepository(TestAppContext);
    var subCategories = await repository.GetByCategoryId(category2.Id);

    // Act
    Assert.Single(subCategories);
    Assert.Equal("SubCategory 1 from category 2", subCategories.FirstOrDefault()?.Name);
  }
}
