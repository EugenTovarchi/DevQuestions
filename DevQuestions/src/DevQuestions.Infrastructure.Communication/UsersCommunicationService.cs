using CSharpFunctionalExtensions;
using DevQuestions.Application.Communication;
using DevQuestions.Shared;

namespace DevQuestions.Infrastructure.Communication
{
    public class UsersCommunicationService : IUsersCommunicationService
    {
        public async Task<Result<long, Failure>> GetUserRatingAsync(Guid userId, CancellationToken ct)
        {
            throw new Exception();
        }
    }
}

