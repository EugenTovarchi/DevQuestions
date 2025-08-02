using DevQuestions.Questions;

namespace DevQuestions.Application.Abstractions;

public  interface IQuestionsReadDbContext
{
    IQueryable<Question> ReadQuestions { get; }
}
