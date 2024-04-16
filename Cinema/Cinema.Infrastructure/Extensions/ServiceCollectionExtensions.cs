using Cinema.Application.Interfaces;
using Cinema.Application.Services;
using Cinema.Domain.Interfaces;
using Cinema.Infrastructure.Repositories;
using Cinema.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Cinema.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region MongoDB
            var connectionString = configuration.GetSection("MongoDbSettings:ConnectionString").Value;
            var databaseName = configuration.GetSection("MongoDbSettings:DatabaseName").Value;

            if (connectionString == null || databaseName == null)
            {
                throw new Exception("MongoDb settings not found in configuration");
            }

            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                return new MongoClient(connectionString);
            });

            services.AddSingleton(serviceProvider =>
            {
                var client = serviceProvider.GetRequiredService<IMongoClient>();
                return client.GetDatabase(databaseName);
            });
            #endregion

            services.AddScoped<IFilmeService, FilmeService>();
            services.AddScoped<IFilmeRepository, FilmeRepository>();
            services.AddScoped<MongoDbService>();

            return services;
        }
    }
}