using DevQuestions.Application.Abstractions;
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
        var assembly = typeof(DependencyInjection).Assembly;

        services.Scan(scan => scan
             .FromAssemblies(assembly)
             .AddClasses(classes => classes
                 .AssignableTo(typeof(ICommandHandler<,>)))
             .AsSelf()
             .WithScopedLifetime()
             .AddClasses(classes => classes
                 .AssignableTo(typeof(ICommandHandler<>)))
             .AsSelf()
             .WithScopedLifetime());

        return services;
    }
}
