using Microsoft.EntityFrameworkCore;

namespace Tags.Infrastructure.Postgres;

public class TagsDbContext : DbContext, ITagsReadDbContext
{
    public DbSet<Tag> Tags { get; set; }

    public IQueryable<Tag> TagsRead => Tags.AsNoTracking().AsQueryable();

}
