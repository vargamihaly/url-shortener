using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Domain.Interfaces;
using UrlShortener.Infrastructure.Persistence;

namespace UrlShortener.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<UrlMapContext>(options =>
            options.UseInMemoryDatabase("UrlShortenerDb"));

        services.AddScoped<IUrlMapDb, EfUrlMapDb>();
        return services;
    }
}