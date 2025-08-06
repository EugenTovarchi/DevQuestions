using DevQuestions.Contracts.Questions;

namespace DevQuestions.Contracts.Responses;

public class QuestionResponse(IEnumerable<QuestionDto> Questions, long TotalCount);

