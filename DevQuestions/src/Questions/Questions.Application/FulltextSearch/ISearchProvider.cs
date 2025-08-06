using CSharpFunctionalExtensions;
using Questions.Domain;
using Shared;

namespace Questions.Application.FulltextSearch;

public interface ISearchProvider
{
    Task<List<Guid>> SearchAsync(string query);

    Task<UnitResult<Failure>> IndexQuestionAsync(Question question);
}
