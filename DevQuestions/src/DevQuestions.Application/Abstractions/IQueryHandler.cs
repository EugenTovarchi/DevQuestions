namespace DevQuestions.Application.Abstractions;

public interface IQueryHandler<TResponse,in TQuery>
     where TQuery : IQuery
{
    Task<TResponse> Handle(TQuery query, CancellationToken ct);
}

public interface IQuery;


