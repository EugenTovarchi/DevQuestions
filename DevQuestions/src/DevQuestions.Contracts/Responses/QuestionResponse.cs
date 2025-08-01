using DevQuestions.Contracts.Questions;

namespace DevQuestions.Application.Questions.GetQuestionsQuery;

public class QuestionResponse(IEnumerable<QuestionDto> Questions, long TotalCount);

