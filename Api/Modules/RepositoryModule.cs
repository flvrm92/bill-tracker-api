using Application.Commands.Bills;
using Application.Commands.Categories;
using Application.Commands.SubCategories;
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
    services.AddScoped(typeof(ISubCategoryRepository), typeof(SubCategoryRepository));

    return services;
  }

  public static IServiceCollection RegisterHandlers(this IServiceCollection services)
  {
    services.AddTransient(typeof(CreateBillCommandHandler), typeof(CreateBillCommandHandler));
    services.AddTransient(typeof(UpdateBillCommandHandler), typeof(UpdateBillCommandHandler));
    services.AddTransient(typeof(CreateUpdateCategoryCommandHandler), typeof(CreateUpdateCategoryCommandHandler));
    services.AddTransient(typeof(DeleteCategoryCommandHandler), typeof(DeleteCategoryCommandHandler));

    services.AddTransient(typeof(CreateSubCategoryCommandHandler), typeof(CreateSubCategoryCommandHandler));
    services.AddTransient(typeof(DeleteSubCategoryCommandHandler), typeof(DeleteSubCategoryCommandHandler));
    services.AddTransient(typeof(UpdateSubCategoryCommandHandler), typeof(UpdateSubCategoryCommandHandler));
    
    return services;
  }
}
