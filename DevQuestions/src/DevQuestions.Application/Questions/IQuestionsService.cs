using CSharpFunctionalExtensions;
using DevQuestions.Contracts.Questions;
using DevQuestions.Shared;

namespace DevQuestions.Application.Questions;

public interface IQuestionsService
{
    Task<Result<Guid, Failure>> Create (CreatedQuestionDto createdQuestionDto, CancellationToken cancellationToken);

    Task<Result<Guid, Failure>> AddAnswer(Guid questionId,AddAnswerDto addAnswerDto, CancellationToken cancellationToken);
}