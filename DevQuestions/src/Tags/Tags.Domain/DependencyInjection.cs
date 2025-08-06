using Microsoft.Extensions.DependencyInjection;
using Tags.Domain;

namespace Tags;

public static class DependencyInjection
{
    public static IServiceCollection AddTagDependencies(this IServiceCollection services)
    {
        services.AddScoped<TagsDbContext>();
        return services;
    }
}
 