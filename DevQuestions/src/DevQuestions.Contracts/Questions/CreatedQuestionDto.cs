using System.Text;

namespace DevQuestions.Contracts.Questions;

public record CreatedQuestionDto(string Title, string Text, Guid UserId, Guid[] TagIds);
