namespace Questions.Domain;

public enum QuestionStatus
{
    /// <summary>
    /// Статус открыт.
    /// </summary>
    Open,

    /// <summary>
    /// Статус решен.
    /// </summary>
    Resolved
}

public static class QustionStatusExtension
{
    public static string ToRussianString(this QuestionStatus status) =>
        status switch
        {
            QuestionStatus.Open => "Открыт",
            QuestionStatus.Resolved => "Решен",
            _=>throw new ArgumentOutOfRangeException(nameof(status)),   
        };
}

