using System.Reflection;
using FastEndpoint.Api.Interfaces;
using FastEndpoint.Api.Services;

namespace FastEndpoint.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddScoped<IAccountMapperConfig, AccountMapperConfig>();
        return services;
    }
}