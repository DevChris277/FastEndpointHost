using System.Reflection;

namespace FastEndpoint.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
           
        return services;
    }
}