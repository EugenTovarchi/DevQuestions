using CSharpFunctionalExtensions;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Questions.Application;
using Questions.Domain;
using Shared;
using Shared.Exceptions;
using Shared.Database;

namespace Questions.Infrastructure.Postgres;

public class QuestionsSqlRepository : IQuestionsRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    public QuestionsSqlRepository(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Guid> AddAnswerAsync(Answer answer, CancellationToken cancellationToken)
    {
        const string sql = @"INSERT INTO answers (id, text, user_id, question_id, rating) 
                        VALUES (@Id, @Text, @UserId, @QuestionId, @Rating)";

        using var connection = _sqlConnectionFactory.Create();

        await connection.ExecuteAsync(sql, new
        {
            answer.Id,
            answer.Text,
            answer.UserId,
            answer.QuestionId,
            answer.Rating
        });

        return answer.Id;
    }

    public async Task<Guid> AddQuestionAsync(Question question, CancellationToken cancellationToken)
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
            question.Status
        });
        return question.Id;
    }

    public async Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken)
    {
        const string sql = "DELETE FROM questions WHERE id = @questionId";

        using var connection = _sqlConnectionFactory.Create();

        await connection.ExecuteAsync(sql, new { questionId });
        return questionId;
    }

    public async Task<int> GeOpenUserQuestionsAsync(Guid userId, CancellationToken cancellationToken)
    {
        const string sql = "SELECT COUNT(*) FROM questions WHERE user_id = @userId AND status = @status";

        using var connection = _sqlConnectionFactory.Create();

        return await connection.ExecuteScalarAsync<int>(sql, new
        {
            userId,
            status = QuestionStatus.Open
        });
    }

    public async Task<Question> GetByIdAsync(Guid questionId, CancellationToken cancellationToken)
    {
        const string questionSql = "SELECT * FROM questions WHERE id = @questionId";
        const string answersSql = "SELECT * FROM answers WHERE question_id = @questionId";

        using var connection = _sqlConnectionFactory.Create();

        var question = await connection.QuerySingleOrDefaultAsync<Question>(questionSql, new { questionId }); //wtf??
        if (question == null)
            return null;

        var answers = await connection.QueryAsync<Answer>(answersSql, new { questionId });
        question.Answers = answers.ToList();

        return question;
    }

    public async Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken)
    {
        const string sql = @"
        INSERT INTO questions (id, title, text, user_id, screenshot_id, tags, status, solution_id)
        VALUES (@Id, @Title, @Text, @UserId, @ScreenshotId, @Tags, @Status, @SolutionId)
        ON CONFLICT (id) DO UPDATE 
        SET title = @Title,
            text = @Text,
            user_id = @UserId,
            screenshot_id = @ScreenshotId,
            tags = @Tags,
            status = @Status,
            solution_id = @SolutionId";

        using var connection = _sqlConnectionFactory.Create();

        await connection.ExecuteAsync(sql, new
        {
            question.Id,
            question.Title,
            question.Text,
            question.UserId,
            question.ScreenshotId,
            Tags = question.Tags.ToArray(),
            question.Status,
            SolutionId = question.Solution?.Id
        });

        return question.Id;
    }

    public async Task<Guid> UpdateAsync(Question question, CancellationToken cancellationToken)
    {
        const string sql = @"UPDATE questions 
                        SET title = @Title, 
                            text = @Text, 
                            screenshot_id = @ScreenshotId, 
                            tags = @Tags, 
                            status = @Status,
                            solution_id = @SolutionId
                        WHERE id = @Id";

        using var connection = _sqlConnectionFactory.Create();

        await connection.ExecuteAsync(sql, new
        {
            question.Id,
            question.Title,
            question.Text,
            question.ScreenshotId,
            Tags = question.Tags.ToArray(),
            question.Status,
            SolutionId = question.Solution?.Id
        });

        return question.Id;
    }

     async Task<Result<Question, Failure>> IQuestionsRepository.GetByIdAsync(
        Guid questionId,
        CancellationToken cancellationToken)
     {
            var question = await GetByIdAsync(questionId, cancellationToken);
            return question == null
                ? Errors.General.NotFound(questionId).ToFailure()
                : question;
    }
}
