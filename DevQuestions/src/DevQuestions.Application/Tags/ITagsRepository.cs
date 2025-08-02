using DevQuestions.Tags;

namespace DevQuestions.Application.Tags
{
    public interface ITagsRepository
    {
        Task<IEnumerable<string>> GetTagsAsync(IEnumerable<Guid> tags, CancellationToken cancellationToken); 
    }
}
