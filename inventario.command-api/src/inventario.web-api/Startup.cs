using inventario.business.Configurations;
using inventario.data.Extensions;
using inventario.web_api.Extensions;
using inventario.web_api.Middlewares;
using Newtonsoft.Json;

namespace inventario.web_api
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            var jsonConfigFile = env.IsDevelopment() ? "appsettings.Development.json" : "appsettings.json";

            _configuration = new ConfigurationBuilder()
                                .AddJsonFile(jsonConfigFile)
                                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            JsonConvert.DefaultSettings = () => jsonSettings;

            
            services
                .AddInventarioBusiness()
                .AddInventarioInfra(_configuration)
                .AddInventarioWebApi();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger()
               .UseSwaggerUI();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}