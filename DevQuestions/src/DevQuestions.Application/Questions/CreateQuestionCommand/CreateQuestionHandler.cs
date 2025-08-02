using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using DevQuestions.Application.Exceptions;
using DevQuestions.Application.Extensions;
using DevQuestions.Contracts.Questions;
using DevQuestions.Questions;
using DevQuestions.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DevQuestions.Application.Questions.CreateQuestion;

public  class CreateQuestionHandler : ICommandHandler<Guid,CreateQuestionCommand>
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IValidator<CreatedQuestionDto> _createdQuestionValidator;
    private readonly ILogger<QuestionsService> _logger;
    public CreateQuestionHandler(
       IQuestionsRepository questionsRepository,
       IValidator<CreatedQuestionDto> createdQuestionValidator,
       ILogger<QuestionsService> logger)
    {
        _questionsRepository = questionsRepository;
        _createdQuestionValidator = createdQuestionValidator;
        _logger = logger;
    }
    public async Task <Result<Guid,Failure>> Handle(
       CreateQuestionCommand command,
       CancellationToken cancellationToken)
    {
        var validationResult = await _createdQuestionValidator.ValidateAsync(command.QuestionDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        int openUserQuestionsCount = await _questionsRepository
            .GeOpenUserQuestionsAsync(command.QuestionDto.UserId, cancellationToken);

        if (openUserQuestionsCount > 3)
        {
            throw new ToManyQuestionsException();
        }

        var questionId = Guid.NewGuid();

        var question = new Question(   //конструктор с необходимыми полями прописан в сущности
            questionId,
            command.QuestionDto.Title,
            command.QuestionDto.Text,
            command.QuestionDto.UserId,
            null,
            command.QuestionDto.TagIds
        );

        await _questionsRepository.AddAsync(question, cancellationToken);

        _logger.LogInformation("Created question was with {questionId}", questionId);

        return questionId;
    }
}
