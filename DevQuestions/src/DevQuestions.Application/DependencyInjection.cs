using DevQuestions.Application.Questions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuestions.Application;

/// <summary>
/// Настройка DI контейнера
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddAppApplication (this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddScoped<IQuestionsService, QuestionsService> ();
        return services;
    }
}
