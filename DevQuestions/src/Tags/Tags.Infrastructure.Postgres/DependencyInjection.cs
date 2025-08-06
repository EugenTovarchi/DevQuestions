using Microsoft.Extensions.DependencyInjection;

namespace Tags.Infrastructure.Postgres;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgresInfrastructure (this IServiceCollection services)
    {
        return services;
    }
}
 