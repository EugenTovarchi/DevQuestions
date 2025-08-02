namespace DevQuestions.Shared;

public record Error
{
    public string Code { get; }
    public string Message { get; }
    public ErrorType? Type { get; }
    public string? InvalidField { get; }

    public static Error None = new(string.Empty, string.Empty, ErrorType.None);

    private Error(string code, string message, ErrorType? type, string? invalidField = null)
    {
        Code = code;
        Message = message;
        Type = type;
        InvalidField = invalidField;
    }

    public static Error Validation(string? code, string message, string? invalidField = null) =>
        new(code ?? "value.is.invalid", message, ErrorType.Validation, invalidField);
    public static Error NotFound(string? code, string message, Guid? id) =>
        new(code ?? "record.not.found", message, ErrorType.NotFound);
    public static Error Failure(string? code, string message) =>
        new(code ?? "failure", message, ErrorType.Failure);
    public static Error Conflict(string? code, string message) =>
        new(code ?? "value.is.conflict", message, ErrorType.Conflict);

    public Failure ToFailure() => new([this]);
}

public enum ErrorType
{
    None,
    Validation,
    NotFound,
    Failure,
    Conflict
}

