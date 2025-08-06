using Framework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Shared.Abstractions;
using Tags.Contracts.Dtos;
using Tags.Domain;

namespace Tags.Features;

public sealed class GetByIds
{
    public sealed class Endpoint : IEndpoint
    {
        public  void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("tags/ids", async (
                GetByIdsDto dto,
                IQueryHandler<IReadOnlyList<TagDto>, GetByIdsQuery> handler,
               CancellationToken cancellationToken) =>
                {
                    var result = await handler.Handle(new GetByIdsQuery(dto), cancellationToken);

                    return Results.Ok(result);
                });
        }

    }

    public sealed class Handler : IQueryHandler<IReadOnlyList<TagDto>, GetByIdsQuery>
        {
        private readonly TagsDbContext _tagsDbContext;

        public Handler(TagsDbContext tagsDbContext) => _tagsDbContext = tagsDbContext;

        public async Task<IReadOnlyList<TagDto>> Handle(
            GetByIdsQuery query,
            CancellationToken ct)
        {
            var tag = await _tagsDbContext.Tags
                .Where(x => query.Dto.Ids.Contains(x.Id))
                .ToListAsync(ct);

            return tag.Select(tags => new TagDto(tags.Id, tags.Name)).ToList();
        }
    }
}
