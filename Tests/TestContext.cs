using Domain.Entities;
using Infra.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Tests;
public class TestContext : DbContext
{
  public readonly ApplicationContext TestAppContext;

  public TestContext()
  {
    var cnn = new SqliteConnection("DataSource=:memory:");
    cnn.Open();

    var options = new DbContextOptionsBuilder<ApplicationContext>().UseSqlite(cnn).Options;

    TestAppContext = new ApplicationContext(options, null);
  }

  public async Task EnsureCreated() => 
    await TestAppContext.Database.EnsureCreatedAsync();

  public async Task CreateSubCategory(SubCategory subCategory)
  {
    await TestAppContext.SubCategories.AddAsync(subCategory);
    await TestAppContext.SaveChangesAsync();
  }

  public async Task CreateCategory(Category category)
  {
    await TestAppContext.Categories.AddAsync(category);
    await TestAppContext.SaveChangesAsync();
  }
}
