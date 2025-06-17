using Microsoft.Extensions.DependencyInjection;
using XpertStore.Data.Extensions.IdentityUser;
using XpertStore.Data.Models.Base;
using XpertStore.Data.Repositories;

namespace XpertStore.Data.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {

        services.AddScoped<IAppIdentityUser, AppIdentityUser>();
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IVendedorRepository, VendedorRepository>();

        return services;
    }
}
