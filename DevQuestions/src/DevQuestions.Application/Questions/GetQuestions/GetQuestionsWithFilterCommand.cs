using DevQuestions.Application.Abstractions;
using DevQuestions.Contracts.Questions;

namespace DevQuestions.Application.Questions.GetQuestionsQuery;

public record GetQuestionsWithFilterCommand(QuestionDto Dto) : ICommand;

