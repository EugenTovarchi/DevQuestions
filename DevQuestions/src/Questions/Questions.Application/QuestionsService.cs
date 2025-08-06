using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Questions.Contracts;
using Questions.Domain;
using Shared;
using Shared.Communication;
using Shared.Database;
using Shared.Exceptions;
using Shared.Extensions;

namespace Questions.Application;

/// <summary>
/// UseCases
/// </summary>
public class QuestionsService : IQuestionsService
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IValidator<CreatedQuestionDto> _createdQuestionValidator;
    private readonly IValidator<AddAnswerDto> _addAnswerValidator;
    private readonly ITransactionManager _transactionManager;
    private readonly ILogger<QuestionsService> _logger;
    private readonly IUsersCommunicationService _usersCommunicationService;

    public QuestionsService(
        IQuestionsRepository questionsRepository,
        IValidator<CreatedQuestionDto> createdQuestionValidator,
        IValidator<AddAnswerDto> addAnswerValidator,
        ITransactionManager transactionManager,
        IUsersCommunicationService usersCommunicationService,
        ILogger<QuestionsService> logger)
    {
        _questionsRepository = questionsRepository;
        _createdQuestionValidator = createdQuestionValidator;
        _addAnswerValidator = addAnswerValidator;
        _transactionManager = transactionManager;
        _usersCommunicationService = usersCommunicationService;
        _logger = logger;
    }

    public async Task<Result<Guid,Failure>> Create(
       CreatedQuestionDto questionDto,
       CancellationToken cancellationToken)
    {
        var validationResult = await _createdQuestionValidator.ValidateAsync(questionDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        int openUserQuestionsCount = await _questionsRepository
            .GeOpenUserQuestionsAsync(questionDto.UserId, cancellationToken);

        if (openUserQuestionsCount > 3)
        {
            throw new ToManyQuestionsException();
        }

        var questionId = Guid.NewGuid();

        var question = new Question(   //конструктор с необходимыми полями прописан в сущности
            questionId,
            questionDto.Title,
            questionDto.Text,
            questionDto.UserId,
            null,
            questionDto.TagIds
        );

        await _questionsRepository.AddAsync(question, cancellationToken);

        _logger.LogInformation("Created question was with {questionId}", questionId);

        return questionId;
    }

    //public async Task<IActionResult> Update(
    //     Guid questionId,
    //     UpdateQuestionDto request,
    //    CancellationToken cancellationToken)
    //{

    //}

    //public async Task<IActionResult> Delete(
    //    Guid questionId,
    //    CancellationToken cancellationToken)
    //{

    //}

    //public async Task<IActionResult> SelectSolution(
    //     Guid questionId,
    //     Guid answerId,
    //    CancellationToken cancellationToken)
    //{

    //}


    public async Task<Result<Guid,Failure>> AddAnswer(
        Guid questionId,
        AddAnswerDto addAnswerDto,
        CancellationToken cancellationToken)
    {
        var validationResult = await _addAnswerValidator.ValidateAsync(addAnswerDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        var userRatingResult  = await _usersCommunicationService.GetUserRatingAsync(addAnswerDto.UserId, cancellationToken);
        if (userRatingResult.IsFailure)
        {
            return userRatingResult.Error;
        }

        if(userRatingResult.Value < 0)
        {
            return Errors.Users.NotEnoughRaiting().ToFailure();
        }

        var qustionIdResult = await _questionsRepository.GetByIdAsync(questionId, cancellationToken);
        if (qustionIdResult.IsFailure)
            return qustionIdResult.Error;

        var transaction = await _transactionManager.BeginTransactionAsync(cancellationToken);
        var answer = new Answer(Guid.NewGuid(), addAnswerDto.UserId, addAnswerDto.Text, questionId);

        var question = qustionIdResult.Value; 
        question.Answers.Add(answer); //Best practice

        var answerId = await _questionsRepository.SaveAsync(question, cancellationToken);

        transaction.Commit();

        _logger.LogInformation("Answer with id {answerId}  was added  to question: {questionId}", answerId, questionId);

        return answerId;
    }
}
