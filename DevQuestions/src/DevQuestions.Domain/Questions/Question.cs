namespace DevQuestions.Domain.Questions;

public class Question
{
    public Question(
        Guid id,
        string title,
        string text,
        Guid userId,
        Guid? screenshotId,
        IEnumerable<Guid> tags)  //Добавляем  IEnumerable т.к. он есть у всех коллекций.Логика приведения 
    {
        Id = id;
        Title = title;
        Text = text;
        UserId = userId;
        ScreenshotId = screenshotId;
        Tags = tags.ToList();
    }
    public Guid Id { get; set; } //не делаем required т.к. его может сгенерить и в БД и мы сами

    public string Title { get; set; }

    public string Text { get; set; }

    public Guid UserId { get; set; }

    public Guid? ScreenshotId { get; set; }

    public List<Answer> Answers { get; set; } = [];

    public Answer? Solution { get; set; }   //указываем прямую ссылку т.к. обе сущности в одном модуле

    public List<Guid> Tags { get; set; }  //указываем через Guid т.к проект будет разростаться (иначе бы делали прямую ссылку List<Tag> Tags

    public QuestionStatus Status { get; set; } = QuestionStatus.Open;
}

