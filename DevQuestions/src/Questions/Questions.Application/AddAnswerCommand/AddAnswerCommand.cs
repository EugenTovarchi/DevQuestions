using Questions.Contracts;
using Shared.Abstractions;

namespace Questions.Application.AddAnswerCommand;

public record AddAnswerCommand(Guid QuestionId, AddAnswerDto AddAnswerDto) : ICommand;

