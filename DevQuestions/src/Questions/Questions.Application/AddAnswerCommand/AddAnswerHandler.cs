using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using FluentValidation;
using Questions.Contracts;
using Shared.Database;
using Shared.Extensions;
using Shared.Communication;
using Shared.Abstractions;
using Shared;
using Questions.Domain;

namespace Questions.Application.AddAnswerCommand;

public  class AddAnswerHandler : ICommandHandler<Guid ,AddAnswerCommand>
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IValidator<AddAnswerDto> _addAnswerValidator;
    private readonly ITransactionManager _transactionManager;
    private readonly IUsersCommunicationService _usersCommunicationService;
    private readonly ILogger<QuestionsService> _logger;

    public AddAnswerHandler(
        IQuestionsRepository questionsRepository,
        IValidator<AddAnswerDto> validator,
        ITransactionManager transactionManager,
        IUsersCommunicationService usersCommunicationService,
        ILogger<QuestionsService> logger)
    {
        _questionsRepository = questionsRepository;
        _addAnswerValidator = validator;
        _transactionManager = transactionManager;
        _usersCommunicationService = usersCommunicationService;
        _logger = logger;
    }
    public async Task<Result<Guid, Failure>> Handle(
       AddAnswerCommand command,
       CancellationToken cancellationToken)
    {
        var validationResult = await _addAnswerValidator.ValidateAsync(command.AddAnswerDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        var userRatingResult = await _usersCommunicationService.GetUserRatingAsync(command.AddAnswerDto.UserId, cancellationToken);
        if (userRatingResult.IsFailure)
        {
            return userRatingResult.Error;
        }

        if (userRatingResult.Value < 0)
        {
            return userRatingResult.Error;
            
        }

        var qustionIdResult = await _questionsRepository.GetByIdAsync(command.QuestionId, cancellationToken);
        if (qustionIdResult.IsFailure)
            return qustionIdResult.Error;

        var transaction = await _transactionManager.BeginTransactionAsync(cancellationToken);
        var answer = new Answer(Guid.NewGuid(), command.AddAnswerDto.UserId, command.AddAnswerDto.Text, command.QuestionId);

        var question = qustionIdResult.Value;
        question.Answers.Add(answer); //Best practice

        var answerId = await _questionsRepository.SaveAsync(question, cancellationToken);

        transaction.Commit();

        _logger.LogInformation("Answer with id {answerId}  was added  to question: {questionId}", answerId, command.QuestionId);

        return answerId;
    }

}
