namespace Questions.Contracts;

public record QuestionsDto(Guid[] TagIds, int page, int pageSize);

