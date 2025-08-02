using DevQuestions.Tags;

namespace DevQuestions.Application.Abstractions;

public interface ITagsReadDbContext
{
    IQueryable<Tag> TagsRead { get; }
}