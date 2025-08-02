namespace DevQuestions.Contracts.Questions;

public record QuestionsDto(Guid[] TagIds, int page, int pageSize);

