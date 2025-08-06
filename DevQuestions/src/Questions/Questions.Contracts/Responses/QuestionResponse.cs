namespace Questions.Contracts.Responses;

public class QuestionResponse(IEnumerable<QuestionDto> Questions, long TotalCount);

