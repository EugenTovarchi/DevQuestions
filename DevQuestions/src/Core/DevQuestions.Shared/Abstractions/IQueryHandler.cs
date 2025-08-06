﻿namespace Shared.Abstractions;

public interface IQueryHandler<TResponse,in TQuery>
     where TQuery : IQuery
{
    Task<TResponse> Handle(TQuery query, CancellationToken ct = default);
}

public interface IQuery;


