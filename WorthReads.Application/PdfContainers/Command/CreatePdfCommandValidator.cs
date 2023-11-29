using FluentValidation;

namespace Application.PdfContainers.Command;

public class CreatePdfCommandValidator : AbstractValidator<CreatePdfContainerCommand>
{
    public CreatePdfCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
