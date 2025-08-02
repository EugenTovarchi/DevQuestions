using DevQuestions.Shared;
using System.Text.Json;

namespace DevQuestions.Application.Exceptions;

public class BadRequestException :Exception
{
    protected BadRequestException(Error[] errors) : base(JsonSerializer.Serialize(errors))
    { }
}
