using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Questions.Application;
using Questions.Application.GetQuestionsQuery;
using Questions.Domain;
using Shared;
using Shared.Exceptions;

namespace Questions.Infrastructure.Postgres;

public class QuestionsEFCoreRepository : IQuestionsRepository
{
    private readonly QuestionsDbContext _dbContext;
    public QuestionsEFCoreRepository(QuestionsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> AddAnswerAsync(Answer answer, CancellationToken cancellationToken)
    {
        var question = await _dbContext.Questions
            .Include(q => q.Answers) 
            .FirstOrDefaultAsync(q => q.Id == answer.QuestionId, cancellationToken);

        if (question is null)
        {
           Errors.General.NotFound(answer.QuestionId);
        }

        question!.Answers.Add(answer);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return answer.Id;
    }

    public async Task<Guid> AddQuestionAsync(Question question, CancellationToken cancellationToken)
    {
        await _dbContext.Questions.AddAsync(question, cancellationToken);  
        await _dbContext.SaveChangesAsync(cancellationToken);
        return question.Id;
    }

    public async Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken)
    {
        var question = await _dbContext.Questions
        .FirstOrDefaultAsync(q => q.Id == questionId, cancellationToken);

        if (question is null)
        {
            Errors.Questions.NotFoundQuestion(questionId);
        }

        _dbContext.Questions.Remove(question!);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return questionId;
    }

    public async Task<int> GeOpenUserQuestionsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Questions
        .CountAsync(q => q.UserId == userId && q.Status == QuestionStatus.Open,
            cancellationToken);
    }

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

    public async Task<(IReadOnlyList<Question> questions, long Count)> GetQuestionsWithFiltersAsync(
        GetQuestionsWithFilterQuery query, CancellationToken cancellationToken)
    {
        var queryable = _dbContext.Questions
        .Include(q => q.Answers)
        .Include(q => q.Solution)
        .AsQueryable();

        var totalCount = await queryable.LongCountAsync(cancellationToken);

        var questions = await queryable.ToListAsync(cancellationToken);

        return (questions, totalCount);
    }

    public async Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken)
    {
         _dbContext.Questions.Attach(question);  
        await _dbContext.SaveChangesAsync(cancellationToken);

        return question.Id;
    }
    public async Task<Guid> UpdateAsync(Question question, CancellationToken cancellationToken)
    {
        _dbContext.Questions.Update(question);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return question.Id;
    }
}
