using Application.Interfaces;
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
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
            );
        });


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<TransacaoService>();


        return services;
    }
}
