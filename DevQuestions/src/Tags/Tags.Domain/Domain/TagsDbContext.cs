using Microsoft.EntityFrameworkCore;

namespace Tags.Domain;

public class TagsDbContext : DbContext
{
    public DbSet<Tag> Tags { get; set; }
}
