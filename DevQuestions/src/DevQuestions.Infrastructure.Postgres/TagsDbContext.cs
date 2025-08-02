using DevQuestions.Application.Abstractions;
using DevQuestions.Tags;
using Microsoft.EntityFrameworkCore;

namespace DevQuestions.Infrastructure.Postgres;

public class TagsDbContext : DbContext, ITagsReadDbContext
{
    public DbSet<Tag> Tags { get; set; }

    public IQueryable<Tag> TagsRead => Tags.AsNoTracking().AsQueryable();

}
