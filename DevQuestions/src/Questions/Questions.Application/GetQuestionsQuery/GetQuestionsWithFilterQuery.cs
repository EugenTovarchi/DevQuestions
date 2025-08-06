using Questions.Contracts;
using Shared.Abstractions;

namespace Questions.Application.GetQuestionsQuery;

public record GetQuestionsWithFilterQuery(QuestionDto Dto) : IQuery;

