using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMS.Application.Interfaces;
using SMS.Application.Interfaces.Repositories;
using SMS.Infrastructure.Repositories;
using SMS.Infrastructure.Services;

namespace SMS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IPersonRepository, PersonRepository>();

        return services;
    }
}