using Questions.Domain;

namespace Shared.Abstractions;

public  interface IQuestionsReadDbContext
{
    IQueryable<Question> ReadQuestions { get; }
}
