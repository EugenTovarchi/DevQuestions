
namespace Tags.Contracts.Dtos;

public class TagsContract : ITagsContract
{
    private readonly TagsDbContext _context;

    public TagsContract()
    {
        

    }
    public Task CreateTag(CreateTagDto dto) => throw new NotImplementedException();
    public Task<IReadOnlyList<TagDto>> GetTags(GetByIdsDto dto) => throw new NotImplementedException();
}
