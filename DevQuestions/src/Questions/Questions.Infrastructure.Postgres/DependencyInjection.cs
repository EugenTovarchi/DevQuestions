using Microsoft.Extensions.DependencyInjection;
using Questions.Application;

namespace Questions.Infrastructure.Postgres;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgresInfrastructure (this IServiceCollection services)
    {
        //services.addscoped<iquestionsrepository, questionsefcorerepository>();
        services.AddScoped<IQuestionsRepository, QuestionsEFCoreRepository>();

        services.AddDbContext<QuestionsDbContext>();

        return services;
    }
}
 