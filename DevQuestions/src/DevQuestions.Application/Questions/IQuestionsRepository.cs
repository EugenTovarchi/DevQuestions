using DevQuestions.Questions;

namespace DevQuestions.Application.Questions;

public interface IQuestionsRepository
{
    Task<Guid> AddAsync(Question question, CancellationToken cancellationToken);
    Task<Guid> UpdateAsync (Question question, CancellationToken cancellationToken);

    Task<Guid> DeleteAsync (Guid questionId, CancellationToken cancellationToken);

    Task<Question> GetByIdAsync(Guid questionId, CancellationToken cancellationToken);

    Task<int> GeOpenUserQuestionsAsync (Guid userId, CancellationToken cancellationToken); // зарпашиваем вопросы юзера с статусом: Open
}
