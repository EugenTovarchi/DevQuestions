using CSharpFunctionalExtensions;
using DevQuestions.Shared;

namespace DevQuestions.Application.Communication;

public interface IUsersCommunicationService
{
    Task<Result<long, Failure>> GetUserRatingAsync (Guid userId, CancellationToken ct = default);
}
