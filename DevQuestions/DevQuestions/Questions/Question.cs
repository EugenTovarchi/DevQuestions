namespace DevQuestions.Questions;

public class Question
{
    public Guid Id { get; set; } //не делаем required т.к. его может сгенерить и в БД и мы сами

    public required string Title { get; set; }

    public required string Text { get; set; }

    public Guid? ScreenshotId { get; set; } 

    public Guid UserId { get; set; }

    public List<Answer> Answers { get; set; } = []; //список ответов к вопросу

    public Answer?  Solution { get; set; }   //указываем прямую ссылку т.к. обе сущности в одном модуле

    public List<Guid> Tegs { get; set; } = []; //указываем через Guid т.к проект будет разростаться (иначе бы делали прямую ссылку List<Tag> Tags

}

