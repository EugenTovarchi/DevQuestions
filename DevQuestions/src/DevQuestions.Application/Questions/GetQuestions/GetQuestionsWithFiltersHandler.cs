using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using DevQuestions.Application.FileStorage;
using DevQuestions.Application.Questions.GetQuestionsQuery;
using DevQuestions.Application.Tags;
using DevQuestions.Contracts.Questions;
using DevQuestions.Questions;
using DevQuestions.Shared;

namespace DevQuestions.Application.Questions.GetQuestions;

public class GetQuestionsWithFiltersHandler : ICommandHandler<QuestionResponse,GetQuestionsWithFilterCommand>
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IFileProvader _fileProvader;
    private readonly ITagsRepository _tagsRepository;

    public GetQuestionsWithFiltersHandler(
        IQuestionsRepository questionsRepository,
        IFileProvader fileProvader,
        ITagsRepository tagsRepository)
    {
        _questionsRepository = questionsRepository;
        _fileProvader = fileProvader;
        _tagsRepository = tagsRepository;
    }

    public async Task<Result<QuestionResponse, Failure>> Handle (GetQuestionsWithFilterCommand command, CancellationToken cancellationToken)
    {
        //var questions и кол-во (фильтр)  = достаем из репо. В репо метод также принимает наш command
        (IReadOnlyList<Question> questions, long count) = await _questionsRepository.GetQuestionsWithFiltersAsync(command, cancellationToken);

        //получаем список id скриншотов, из тех что существуют
        var screenshotIds = questions
            .Where(q => q.ScreenshotId is not null)
            .Select(q => q.ScreenshotId!.Value); //полюбому есть ибо выше проверка

        //Получаем список q.ScreenshotId и их url
        var fileDict = await _fileProvader.GetUrlByIdAsync(screenshotIds, cancellationToken);

        var questionsTags = questions.SelectMany(q=>q.Tags);

        //получаем список тегов в формате string
        var tags = await _tagsRepository.GetTagsAsync(questionsTags, cancellationToken);

        var questionDto = questions.Select(q => new QuestionDto(
            q.Id,
            q.Title,
            q.Text,
            q.UserId,
            q.ScreenshotId is not null ? fileDict[q.ScreenshotId.Value] : null,
            q.Solution?.Id,
            tags,
            q.Status.ToRussianString()  //переводим enum status int=>string на русском
            ));

            return new QuestionResponse(questionDto, count);
    }
}
