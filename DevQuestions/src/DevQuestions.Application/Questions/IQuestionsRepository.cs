using CSharpFunctionalExtensions;
using DevQuestions.Application.Questions.GetQuestionsQuery;
using DevQuestions.Domain.Questions;
using DevQuestions.Shared;

namespace DevQuestions.Application.Questions;

public interface IQuestionsRepository
{
    Task<Guid> AddAsync(Question question, CancellationToken cancellationToken);
    Task<Guid> AddAnswerAsync(Answer answer, CancellationToken cancellationToken);
    Task<Guid> UpdateAsync (Question question, CancellationToken cancellationToken);

    Task<Guid> DeleteAsync (Guid questionId, CancellationToken cancellationToken);

    Task<Result<Question,Failure>> GetByIdAsync(Guid questionId, CancellationToken cancellationToken);

    //Task<(IReadOnlyList<Question> questions, long Count)>  GetQuestionsWithFiltersAsync
    //    (GetQuestionsWithFilterCommand command, CancellationToken cancellationToken);

    Task<int> GeOpenUserQuestionsAsync (Guid userId, CancellationToken cancellationToken); // зарпашиваем вопросы юзера с статусом: Open
    Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken);
}
