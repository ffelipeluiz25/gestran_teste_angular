using GestranApi.Context;
using GestranApi.Repository;
using GestranApi.Repository.Interface;
using GestranApi.Service;
using GestranApi.Service.Interface;
namespace GestranApi.Helpers.Configuration
{
    public static class DependencyInjectionConfig
    {

        public static IServiceCollection ResolveDependenciesRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IChecklistRepository, ChecklistRepository>();
            services.AddTransient<IChecklistItemRepository, ChecklistItemRepository>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddScoped<GestranDbContext>();
            return services;
        }

        public static IServiceCollection ResolveDependenciesServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseServices<>), typeof(BaseServices<>));
            services.AddTransient<IChecklistService, ChecklistService>();
            services.AddTransient<IChecklistItemService, ChecklistItemService>();
            services.AddTransient<IItemService, ItemService>();
            return services;
        }
    }
}