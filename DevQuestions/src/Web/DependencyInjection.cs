using Questions.Presenters;
using Tags;

namespace Web;

/// <summary>
/// Настройка DI контейнера.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddProgramDependencies(this IServiceCollection services)
    {
        services.AddWebDependencies();
        services.AddTagDependencies();
        services.AddQuestionsModules();
        return services;
    }

    private static IServiceCollection AddWebDependencies(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddQuestionsModules();
        return services;
    }

}
