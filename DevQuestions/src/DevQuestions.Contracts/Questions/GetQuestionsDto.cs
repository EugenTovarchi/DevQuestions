namespace DevQuestions.Contracts.Questions;

public record GetQuestionsDto(Guid[] TagIds, int page, int pageSize);

