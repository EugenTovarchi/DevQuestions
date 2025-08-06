using CSharpFunctionalExtensions;
using Shared;
using Shared.Communication;

namespace Infrastructure.Communication
{
    public class UsersCommunicationService : IUsersCommunicationService
    {
        public async Task<Result<long, Failure>> GetUserRatingAsync(Guid userId, CancellationToken ct)
        {
            throw new Exception();
        }
    }
}

