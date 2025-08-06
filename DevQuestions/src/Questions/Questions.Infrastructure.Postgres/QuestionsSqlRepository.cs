using CSharpFunctionalExtensions;
using Dapper;
using Questions.Application;
using Questions.Domain;
using Shared;
using Shared.Database;

namespace Questions.Infrastructure.Postgres;

public class QuestionsSqlRepository : IQuestionsRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    public QuestionsSqlRepository(ISqlConnectionFactory  sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public Task<Guid> AddAnswerAsync(Answer answer, CancellationToken cancellationToken) => throw new NotImplementedException();

    public  async Task<Guid> AddAsync(Question question, CancellationToken cancellationToken)
    {
        const string sql = "INSERT INTO questions (id, title, user_id, screenshot_id, tags, status) " +
            "VALUES (@Id, @Title, @UserId, @ScreenshotId, @Tags, @Status)";

        using var connection = _sqlConnectionFactory.Create();

        await connection.ExecuteAsync(sql, new
        {
            question.Id,
            question.Title,
            question.Text,
            question.UserId,
            question.ScreenshotId,
            Tags = question.Tags.ToArray(),
            Status = question.Status
        });
        return question.Id;
    }

    public async Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GeOpenUserQuestionsAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Question> GetByIdAsync(Guid questionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken) => throw new NotImplementedException();

    public async Task<Guid> UpdateAsync(Question question, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<Result<Question, Failure>> IQuestionsRepository.GetByIdAsync(Guid questionId, CancellationToken cancellationToken) => throw new NotImplementedException();
}
