using Microsoft.Extensions.DependencyInjection;
using Questions.Application;
using Questions.Infrastructure.Postgres;

namespace Questions.Presenters;

public static class DependencyInjection
{
    public static IServiceCollection AddQuestionsModules (this IServiceCollection services)
    {
        services.AddAppApplication();
        services.AddPostgresInfrastructure();

        return services;
    }
}
