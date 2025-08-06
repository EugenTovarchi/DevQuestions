using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Questions.Contracts;
using Questions.Contracts.Responses;
using Questions.Domain;
using Shared.Abstractions;
using Shared.FileStorage;
using Tags.Contracts;
using Tags.Contracts.Dtos;

namespace Questions.Application.GetQuestionsQuery;

public class GetQuestionsWithFiltersHandler : IQueryHandler<QuestionResponse,GetQuestionsWithFilterQuery>
{
    private readonly IQuestionsReadDbContext _questionsReadDbContext;
    private readonly IFileProvader _fileProvader;
    private readonly ITagsContract _tagsContract;

    public GetQuestionsWithFiltersHandler(
        IQuestionsReadDbContext questionsReadDbContext,
        IFileProvader fileProvader,
        ITagsContract tagsContract)
    {
        _questionsReadDbContext = questionsReadDbContext;
        _fileProvader = fileProvader;
        _tagsContract = tagsContract;
    }

    public async Task<QuestionResponse> Handle (GetQuestionsWithFilterQuery query, CancellationToken cancellationToken)
    {
        //var questions и кол-во (фильтр)  = достаем из репо. В репо метод также принимает наш command
        var questions = await _questionsReadDbContext.ReadQuestions.Include(q => q.Id).ToListAsync(cancellationToken);

        long count = await _questionsReadDbContext.ReadQuestions.LongCountAsync(cancellationToken); //типо все получаем что есть ?

        //получаем список id скриншотов, из тех что существуют
        var screenshotIds = questions
            .Where(q => q.ScreenshotId is not null)
            .Select(q => q.ScreenshotId!.Value); //полюбому есть ибо выше проверка

        //Получаем список q.ScreenshotUrl
        var fileDict = await _fileProvader.GetUrlByIdAsync(screenshotIds, cancellationToken);

        var questionsTags = questions.SelectMany(q=>q.Tags);

        //получаем список тегов в формате string
        var tags = await _tagsContract.GetByIds(new GetByIdsDto(questionsTags.ToArray()));

        var questionDto = questions.Select(q => new QuestionDto(
            q.Id,
            q.Title,
            q.Text,
            q.UserId,
            q.ScreenshotId is not null ? fileDict[q.ScreenshotId.Value] : null,
            q.Solution?.Id,
            tags.Select(tags=>tags.Name),
            q.Status.ToRussianString()  //переводим enum status int=>string на русском
            ));

            return new QuestionResponse(questionDto, count);
    }
}

