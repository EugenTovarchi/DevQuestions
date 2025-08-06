using Dapper;
using DevQuestions.Application.Database;
using DevQuestions.Application.Questions;
using DevQuestions.Domain.Questions;

namespace DevQuestions.Infrastructure.Postgres.Repositories;

public class QuestionsSqlRepository : IQuestionsRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    public QuestionsSqlRepository(ISqlConnectionFactory  sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public  async Task<Guid> AddAsync(Question question, CancellationToken cancellationToken)
    {
        const string sql = "INSERT INTO questions (id, title, user_id, screenshot_id, tags, status) " +
            "VALUES (@Id, @Title, @UserId, @ScreenshotId, @Tags, @Status)";

        using var connection = _sqlConnectionFactory.Create();

        await connection.ExecuteAsync(sql, new
        {
            Id = question.Id,
            Title = question.Title,
            Text = question.Text,
            UserId = question.UserId,
            ScreenshotId = question.ScreenshotId,
            Tags = question.Tags.ToArray(),
            Status = question.status
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

    public async Task<Guid> UpdateAsync(Question question, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
