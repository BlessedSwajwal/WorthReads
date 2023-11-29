using FluentValidation;

namespace Application.PdfContainers.Command.AddPDF;

public class AddReadCommandValidator : AbstractValidator<AddReadCommand>
{
    public AddReadCommandValidator()
    {
        RuleFor(c => c.User).NotEmpty();
        RuleFor(c => c.ContainerId).NotEmpty();
        RuleFor(c => c.url).NotEmpty();
    }
}
