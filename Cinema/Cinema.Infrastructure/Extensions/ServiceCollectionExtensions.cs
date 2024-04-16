using Cinema.Application.Interfaces;
using Cinema.Application.Services;
using Cinema.Domain.Interfaces;
using Cinema.Infrastructure.Repositories;
using Cinema.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cinema.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("MongoDbSettings:ConnectionString").Value;
            var databaseName = configuration.GetSection("MongoDbSettings:DatabaseName").Value;

            if (connectionString == null || databaseName == null)
            {
                throw new Exception("MongoDb settings not found in configuration");
            }

            services.AddSingleton(new MongoDbSettings(connectionString, databaseName));

            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            });

            services.AddScoped<IFilmeService, FilmeService>();
            services.AddScoped<IFilmeRepository, FilmeRepository>();

            return services;
        }
    }
}