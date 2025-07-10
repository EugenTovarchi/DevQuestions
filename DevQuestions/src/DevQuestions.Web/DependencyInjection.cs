using DevQuestions.Application;
using DevQuestions.Application.Questions;
using FluentValidation;

namespace DevQuestions.Web;

/// <summary>
/// Настройка DI контейнера
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddProgramDependencies(this IServiceCollection services) =>
        services.AddWebDependencies()
                .AddAppApplication();

    private static IServiceCollection AddWebDependencies(this IServiceCollection services)
    {
        services.AddControllers();

        return services;
    }

}
