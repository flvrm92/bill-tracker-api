using Domain.Entities;
using Domain.Entities.Bills;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Infra.Context;
public class ApplicationContext(
  DbContextOptions<ApplicationContext> options,
  IConfiguration configuration)
  : DbContext(options)
{
  #region dbSets
  public DbSet<Bill> Bills { get; set; }
  public DbSet<BillItem> BillItems { get; set; }
  public DbSet<Category> Categories { get; set; }
  public DbSet<SubCategory> SubCategories { get; set; }

  #endregion

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    if (optionsBuilder.IsConfigured)
      return;

    SqlConnection connection = new()
    {
      ConnectionString = configuration.GetConnectionString("Default") ?? string.Empty
    };

    optionsBuilder.UseSqlServer(connection, x => x.MigrationsAssembly("Infra"));
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }
}
