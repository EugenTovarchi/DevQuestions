using DevQuestions.Contracts.Questions;
using DevQuestions.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DevQuestions.Application.Questions;

public class QuestionsService : IQuestionsService
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly  IValidator<CreatedQuestionDto> _validator;
    private readonly ILogger<QuestionsService> _logger;

    public QuestionsService(
        IQuestionsRepository questionsRepository,
        IValidator<CreatedQuestionDto> validator,
        ILogger<QuestionsService> logger)
    {
        _questionsRepository = questionsRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Guid> Create(
       CreatedQuestionDto questionDto,
       CancellationToken cancellationToken)
    {
        //Проверка валидности(FluentValidation library)  входных данных
        var validationResult = await _validator.ValidateAsync(questionDto, cancellationToken);
        if(!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        //проверка валидности бизнес логики
        int openUserQuestionsCount = await _questionsRepository
            .GeOpenUserQuestionsAsync(questionDto.UserId, cancellationToken);

        if (openUserQuestionsCount > 3)
        {
            throw new Exception("Пользователь не может отрыть больше 3х открытых вопросов");
        }

        //создание сущности Question
        var questionId = Guid.NewGuid();

        var question = new Question(   //конструктор с необходимыми полями прописан в сущности
            questionId,
            questionDto.Title,
            questionDto.Text,
            questionDto.UserId,
            null,
            questionDto.TagIds
        );

        //Сохранение сущности Question в БД
        await _questionsRepository.AddAsync(question, cancellationToken);
        return questionId; 

        //Логирование об успешном или неуспешном сохранении 
        _logger.LogInformation("Created question was with {questionId}", questionId);
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


    //public async Task<IActionResult> AddAnswer(
    //    Guid questionId,
    //    AddAnswerDto answerDto,
    //    CancellationToken cancellationToken)
    //{

    //}
}
