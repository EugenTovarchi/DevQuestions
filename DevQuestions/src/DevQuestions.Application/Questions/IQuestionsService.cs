using DevQuestions.Contracts.Questions;

namespace DevQuestions.Application.Questions;

public interface IQuestionsService
{
    Task<Guid> Create (CreatedQuestionDto createdQuestionDto, CancellationToken cancellationToken);
}