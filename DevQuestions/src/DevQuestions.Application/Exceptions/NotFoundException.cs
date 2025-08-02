using DevQuestions.Shared;
using System.Text.Json;

namespace DevQuestions.Application.Exceptions;

public class NotFoundException : Exception
{
    protected NotFoundException(Error[] errors) : base(JsonSerializer.Serialize(errors))
    { }
}
