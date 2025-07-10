using DevQuestions.Contracts.Questions;
using FluentValidation;

namespace DevQuestions.Application.Questions;

public class CreateQuestionValidator : AbstractValidator<CreatedQuestionDto>
{
    public CreateQuestionValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(500).WithMessage("Title isn't validate");
        RuleFor(x=>x.Text).NotEmpty().MaximumLength(5000).WithMessage("Title isn't validate");
        RuleFor(x => x.UserId).NotEmpty();
    }
}
