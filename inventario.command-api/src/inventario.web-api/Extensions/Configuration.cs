using inventario.web_api.Validators;

namespace inventario.web_api.Extensions
{
    public static class Configuration
    {
        public static IServiceCollection AddInventarioWebApi(this IServiceCollection services)
        {
            services.AddControllers();

            return services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddScoped<CategoriaValidator>()
                .AddScoped<ProdutoValidator>();
        }
    }
}
