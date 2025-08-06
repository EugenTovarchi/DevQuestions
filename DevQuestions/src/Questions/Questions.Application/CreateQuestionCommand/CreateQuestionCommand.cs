using Questions.Contracts;
using Shared.Abstractions;

namespace Questions.Application.CreateQuestionCommand;

public  record  CreateQuestionCommand(CreatedQuestionDto QuestionDto) : ICommand;

