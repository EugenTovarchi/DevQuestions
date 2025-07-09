namespace DevQuestions.Web.Controllers;

public record UpdateQuestionDto(string Title, string Body,  Guid[] TagIds);