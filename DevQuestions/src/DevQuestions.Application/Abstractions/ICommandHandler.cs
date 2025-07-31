using CSharpFunctionalExtensions;
using DevQuestions.Shared;

namespace DevQuestions.Application.Abstractions;

public interface ICommandHandler<TResponse, in TCommand>
        where TCommand : ICommand
{
    Task<Result<TResponse, Failure>> Handle(TCommand command, CancellationToken ct);
}

public interface ICommandHandler< in TCommand>
     where TCommand : ICommand
{
    Task<UnitResult<Failure>> Handle(TCommand command, CancellationToken ct);
}

public interface ICommand;

