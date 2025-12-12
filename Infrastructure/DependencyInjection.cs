using Infrastructure.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MySqlConnector;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var mySqlConnection = new MySqlConnection(
            configuration.GetConnectionString("DefaultConnection"));


              services.AddDbContext<AppDataContext>(options =>
           options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

        return services;
    }
}
