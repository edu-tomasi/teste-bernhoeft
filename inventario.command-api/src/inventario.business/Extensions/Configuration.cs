using inventario.business.Service;
using Microsoft.Extensions.DependencyInjection;

namespace inventario.business.Configurations
{
    public static class Configuration
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            return services
                .AddScoped<ICategoriaService, CategoriaService>()
                .AddScoped<IProdutoService, ProdutoService>();
        }
    }
}
