using Dapper;
using inventario.business.Abstractions.Data;
using inventario.data.Configurations;
using inventario.data.Context;
using inventario.data.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace inventario.data.Extensions
{
    public static class Configuration
    {
        public static IServiceCollection AddInventarioInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDatabaseConfig(configuration)
                .AddScoped<IDbConnection>(provider =>
                {
                    var conn = SqlClientFactory.Instance.CreateConnection();
                    conn.ConnectionString = provider.GetRequiredService<DatabaseConfig>().ConnectionString;
                    return conn;
                })
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IProdutoRepository, ProdutoRepository>()
                .AddScoped<ICategoriaRepository, CategoriaRepository>();

            return services;
        }

        private static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration) =>
            services.AddSingleton(_ =>
            {
                SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);
                var connectionString = configuration.GetSection("ConnectionString:Default").Value;
                var sqlConnection = new SqlConnectionStringBuilder(connectionString)
                {
                    TrustServerCertificate = true,
                    MultiSubnetFailover = true,
                    TransactionBinding = "Implicit Unbind",
                    Enlist = false,
                    MinPoolSize = 100,
                    MaxPoolSize = 100,
                    CommandTimeout = 2,
                };

                return new DatabaseConfig()
                {
                    ConnectionString = sqlConnection.ConnectionString,
                };
            });
    }
}
