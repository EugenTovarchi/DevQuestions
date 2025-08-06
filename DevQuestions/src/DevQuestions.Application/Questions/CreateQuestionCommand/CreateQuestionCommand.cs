using DevQuestions.Application.Abstractions;
using DevQuestions.Contracts.Questions;
namespace DevQuestions.Application.Questions.CreateQuestionCommand;

public  record  CreateQuestionCommand(CreatedQuestionDto QuestionDto) : ICommand;

