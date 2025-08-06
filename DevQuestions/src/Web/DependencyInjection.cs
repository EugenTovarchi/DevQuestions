using Questions.Presenters;

namespace Web;

/// <summary>
/// Настройка DI контейнера.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddProgramDependencies(this IServiceCollection services) =>
        services.AddWebDependencies()
                .AddAppApplication()
                .AddPostgresInfrastructure();

    private static IServiceCollection AddWebDependencies(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddQuestionsModules();
        return services;
    }

}
