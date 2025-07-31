using DevQuestions.Shared;

namespace DevQuestions.Application.Exceptions;

public  class QuestionValidationException : BadRequestException
{
    public QuestionValidationException(Error[] errors ):base(errors)
    {
        
    }
}
