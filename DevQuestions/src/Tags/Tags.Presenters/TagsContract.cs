using Shared.Abstractions;
using Tags.Contracts;
using Tags.Contracts.Dtos;
using Tags.Domain;
using Tags.Features;

namespace Tags.Presenters;

public class TagsContract : ITagsContract
{
    private readonly TagsDbContext _context;
    private readonly IQueryHandler<IReadOnlyList<TagDto>, GetByIdsQuery> _handler;

    public TagsContract(TagsDbContext context, IQueryHandler <IReadOnlyList<TagDto>, GetByIdsQuery> handler)
    {
        _context = context;
        _handler = handler;
    }

    public async Task CreateTag(CreateTagDto dto)
    {
        await Create.Handler(dto, _context);
    }

    public async Task<IReadOnlyList<TagDto>> GetByIds(GetByIdsDto dto)
    {
       return await _handler.Handle(new GetByIdsQuery(dto));
    }
}
