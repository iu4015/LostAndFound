using LostAndFound.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LostAndFound.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<FoundItemService>();
        services.AddScoped<ReturnRequestService>();
        return services;
    }
}
