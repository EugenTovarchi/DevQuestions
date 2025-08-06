using Shared.Abstractions;

namespace Tags.Contracts.Dtos;

public record GetByIdsQuery(GetByIdsDto Dto) : IQuery;