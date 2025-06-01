using FastEndpoint.Api;
using FastEndpoint.Infrastructure;
using FastEndpoint.Infrastructure.Persistence;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();
{
    builder.AddNpgsqlDbContext<FepDataContext>("fastEndpointDb");
    builder.Services
        .AddAuthenticationJwtBearer(s => s.SigningKey = builder.Configuration["JwtSettings:Secret"])
        .AddAuthorization()
        .AddFastEndpoints()
        .AddPresentation(builder.Configuration)
        .AddInfrastructure(builder.Configuration)
        .SwaggerDocument();
}

var app = builder.Build();
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider
            .GetRequiredService<FepDataContext>();
        
        var strategy = dbContext.Database.CreateExecutionStrategy();
        strategy.Execute(() => dbContext.Database.Migrate());
    }
    app
        .UseFastEndpoints()
            
        .UseAuthentication()
        .UseAuthorization()
        .UseSwaggerGen();
    app.Run();
}