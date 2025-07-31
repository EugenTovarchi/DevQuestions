using DevQuestions.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevQuestions.Web.ResponseExtensions;

public static class ResponseExtesions
{
    /// <summary>
    /// Возвращаем статус код который был чаще обнаружен. Используем в Controller.
    /// </summary>
    /// <param name="failure">Ошибки которым мы присваваем статус код.</param>
    /// <returns>Возвращаем самую частую ошибку и её статус код.</returns>
    public static ActionResult ToResponse(this Failure failure)
    {
        if (!failure.Any())
        {
            return new ObjectResult(null)
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }

        var distinctErrorTypes = failure
            .Select(e => e.Type)
            .Distinct() // Возвращает только уникальные значения
            .ToList();

        int statusCode = distinctErrorTypes.Count > 1
            ? StatusCodes.Status500InternalServerError
            : GetStatusCodeFromErrorType(distinctErrorTypes.FirstOrDefault() ?? ErrorType.Failure); // Возвращаем первую ошибку.

        return new ObjectResult(failure)
        {
            StatusCode = statusCode,
        };
    }

    private static int GetStatusCodeFromErrorType(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError,
        };
}
