using DevQuestions.Contracts.Questions;
using FluentValidation;

namespace DevQuestions.Application.Questions.CreateQuestion;

public class CreateQuestionValidator : AbstractValidator<CreatedQuestionDto>
{
    public CreateQuestionValidator()
    {
        RuleFor(q => q.Title).NotEmpty().WithMessage("Title can not be empty!")
            .MaximumLength(500).WithMessage("Title has to be less than 500 symbols");

        RuleFor(q => q.Text).NotEmpty().WithMessage("Text can not be empty!")
            .MaximumLength(5000).WithMessage("Text has to be less than 500 symbols");

        RuleFor(q => q.UserId).NotEmpty().WithMessage("Id is empty!");
    }
}
