using Application.Commands.Bills;
using Application.Commands.Categories;
using Domain.Entities;
using Domain.Entities.Bills;
using Domain.Repositories;
using Domain.Repositories.Bills;
using Infra.Repositories;
using Infra.Repositories.Bills;

namespace Api.Modules;

public static class RepositoryModule
{
  public static IServiceCollection RegisterRepositories(this IServiceCollection services)
  {
    services.AddScoped(typeof(IRepository<Category>), typeof(BaseRepository<Category>));
    services.AddScoped(typeof(IRepository<Bill>), typeof(BaseRepository<Bill>));
    services.AddScoped(typeof(IRepository<BillItem>), typeof(BaseRepository<BillItem>));
    services.AddScoped(typeof(IBillRepository), typeof(BillRepository));

    return services;
  }

  public static IServiceCollection RegisterHandlers(this IServiceCollection services)
  {
    services.AddTransient(typeof(CreateUpdateBillCommandHandler), typeof(CreateUpdateBillCommandHandler));
    services.AddTransient(typeof(CreateUpdateCategoryCommandHandler), typeof(CreateUpdateCategoryCommandHandler));
    services.AddTransient(typeof(DeleteCategoryCommandHandler), typeof(DeleteCategoryCommandHandler));

    return services;
  }
}
