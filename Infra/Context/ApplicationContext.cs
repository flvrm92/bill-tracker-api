using Domain.Entities;
using Domain.Entities.Bills;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infra.Context
{
  public class ApplicationContext: IdentityDbContext
  {

    private readonly IConfiguration _configuration;

    public ApplicationContext(
      DbContextOptions<ApplicationContext> options,
      IConfiguration configuration): base (options)
    {
      _configuration = configuration;
    }

    #region dbSets
    public DbSet<Bill> Bills { get; set; }
    public DbSet<BillItem> BillItems { get; set; }
    public DbSet<Category> Categories { get; set; }

    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (optionsBuilder.IsConfigured)
        return;

      SqlConnection connection = new()
      {
        ConnectionString = _configuration.GetConnectionString("Default")
      };

      optionsBuilder.UseSqlServer(connection, x => x.MigrationsAssembly("Infra"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

  }

}
