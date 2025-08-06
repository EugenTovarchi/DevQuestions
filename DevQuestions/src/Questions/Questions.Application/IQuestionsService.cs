using CSharpFunctionalExtensions;
using Questions.Contracts;
using Shared;

namespace Questions.Application;

public interface IQuestionsService
{
    Task<Result<Guid, Failure>> Create (CreatedQuestionDto createdQuestionDto, CancellationToken cancellationToken);

    Task<Result<Guid, Failure>> AddAnswer(Guid questionId,AddAnswerDto addAnswerDto, CancellationToken cancellationToken);
}