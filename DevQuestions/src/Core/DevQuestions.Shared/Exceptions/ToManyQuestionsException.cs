namespace Shared.Exceptions;

public  class ToManyQuestionsException : BadRequestException
{
    public ToManyQuestionsException() : base([Errors.Questions.ToManyQuestions()])
    {
        
    }
}
