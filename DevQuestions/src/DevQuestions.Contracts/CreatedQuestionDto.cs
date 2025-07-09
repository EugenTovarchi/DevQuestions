using System.Text;

namespace DevQuestions.Web.Controllers;

public record CreatedQuestionDto(string Title, string Body, Guid UserId, Guid[] TagIds);
