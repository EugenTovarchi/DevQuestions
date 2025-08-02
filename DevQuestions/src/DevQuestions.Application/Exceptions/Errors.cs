using DevQuestions.Shared;

namespace DevQuestions.Application.Exceptions;

public partial class Errors
{
    public static class General
    {
        public static Error NotFound(Guid id) =>
            Error.Failure("record.not.found", $"Запись не найдена по id {id}");
    }
    public static class Questions 
    {
        public static Error ToManyQuestions() =>
            Error.Failure("question.too.many", "Пользователь не может открыть более 3х вопросовю");
    }

    public static class Users
    {
        public static Error NotEnoughRaiting() =>
            Error.Failure("raiting.not.enough", "У пользователя недостаточный уровень рейтинга");
    }

}
