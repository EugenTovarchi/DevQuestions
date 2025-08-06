namespace Shared.Exceptions;

public  class QuestionValidationException : BadRequestException
{
    public QuestionValidationException(Error[] errors ):base(errors)
    {
        
    }
}
