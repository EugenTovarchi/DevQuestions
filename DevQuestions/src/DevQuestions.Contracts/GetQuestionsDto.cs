namespace DevQuestions.Web.Controllers;

public record GetQuestionsDto(Guid[] TagIds, int page , int pageSize);

