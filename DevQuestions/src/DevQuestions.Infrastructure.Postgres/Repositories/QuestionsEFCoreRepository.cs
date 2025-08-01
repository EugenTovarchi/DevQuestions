using CSharpFunctionalExtensions;
using DevQuestions.Application.Exceptions;
using DevQuestions.Application.Questions;
using DevQuestions.Application.Questions.GetQuestionsQuery;
using DevQuestions.Questions;
using DevQuestions.Shared;
using Microsoft.EntityFrameworkCore;

namespace DevQuestions.Infrastructure.Postgres.Repositories;

public class QuestionsEFCoreRepository : IQuestionsRepository
{
    private readonly QuestionsDbContext _dbContext;
    public QuestionsEFCoreRepository(QuestionsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Guid> AddAnswerAsync(Answer answer, CancellationToken cancellationToken) => throw new NotImplementedException();

    //public async Task<Guid> AddAnswerAsync(Answer answer, CancellationToken cancellationToken)
    //{
    //    await _dbContext.Questions.AddAsync(answer, cancellationToken);
    //    await _dbContext.SaveChangesAsync();
    //    return answer.Id;
    //}

    public async Task<Guid> AddAsync(Question question, CancellationToken cancellationToken)
    {
        await _dbContext.Questions.AddAsync(question, cancellationToken);  
        await _dbContext.SaveChangesAsync();
        return question.Id;
    }

    public Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<int> GeOpenUserQuestionsAsync(Guid userId, CancellationToken cancellationToken) => throw new NotImplementedException();

    public async Task<Result<Question,Failure>> GetByIdAsync(Guid questionId, CancellationToken cancellationToken)
    {
        var question = await _dbContext.Questions
           .Include(q => q.Answers)
           .Include(q => q.Solution)
           .FirstOrDefaultAsync(q=>q.Id == questionId, cancellationToken);


        if (question is null)
        {
            return Errors.General.NotFound(questionId).ToFailure();
        }

        return question;
    }

    public Task<(IReadOnlyList<Question> questions, long Count)> GetQuestionsWithFiltersAsync(
        GetQuestionsWithFilterCommand command, CancellationToken cancellationToken) 
        => throw new NotImplementedException();

    public async Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken)
    {
         _dbContext.Questions.Attach(question);  
        await _dbContext.SaveChangesAsync();

        return question.Id;
    }
    public Task<Guid> UpdateAsync(Question question, CancellationToken cancellationToken) => throw new NotImplementedException();
}
