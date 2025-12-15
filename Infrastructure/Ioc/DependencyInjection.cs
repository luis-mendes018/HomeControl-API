using Application.Services;

using Domain.Interfaces;

using Infrastructure.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MySqlConnector;

namespace Infrastructure.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var mySqlConnection = new MySqlConnection(
            configuration.GetConnectionString("DefaultConnection"));


        services.AddDbContext<AppDataContext>(options =>
        {
            options
                .UseMySql(
                    mySqlConnection,
                    ServerVersion.AutoDetect(mySqlConnection)
                );
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<TransacaoService>();


        return services;
    }
}
