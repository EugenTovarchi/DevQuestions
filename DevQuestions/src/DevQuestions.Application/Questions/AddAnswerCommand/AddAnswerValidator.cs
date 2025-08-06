using DevQuestions.Contracts.Questions;
using FluentValidation;

namespace DevQuestions.Application.Questions.AddAnswerCommand;

public class AddAnswerValidator : AbstractValidator<AddAnswerDto>
{
    public AddAnswerValidator()
    {
        RuleFor(a => a.Text).NotEmpty().WithMessage("Text can not be empty!")
            .MaximumLength(5000).WithMessage("Text has to be less than 500 symbols");

        RuleFor(a => a.UserId).NotEmpty().WithMessage("User Id is empty!");
    }
}
