using Microsoft.Extensions.DependencyInjection;

namespace MyWebApi.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationCqrs(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));

        return services;
    }
}
