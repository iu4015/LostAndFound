using LostAndFound.Application.Repositories;
using LostAndFound.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LostAndFound.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<LostAndFoundDbContext>(options =>
            options.UseSqlite(connectionString));
        services.AddScoped<IFoundItemRepository, FoundItemRepository>();
        services.AddScoped<IReturnRequestRepository, ReturnRequestRepository>();
        return services;
    }
}
