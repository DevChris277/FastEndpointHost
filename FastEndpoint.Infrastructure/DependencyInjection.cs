
using FastEndpoint.Application.Interfaces.Services;
using FastEndpoint.Domain.Common.Settings;

using FastEndpoint.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FastEndpoint.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services
            .AddProviders(configuration)
            .AddPersistence(configuration)
            .AddAuth(configuration);
        return services;
    }

    public static IServiceCollection AddProviders(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();


        return services;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        // services.AddDbContextFactory<FepContext>(options =>
        //     options.UseNpgsql(configuration.GetConnectionString("FepDb")));

       
        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));

        return services;
    }
}