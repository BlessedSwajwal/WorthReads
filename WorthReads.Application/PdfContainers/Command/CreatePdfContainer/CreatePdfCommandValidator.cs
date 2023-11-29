using FluentValidation;

namespace Application.PdfContainers.Command.CreatePdfContainer;

public class CreatePdfCommandValidator : AbstractValidator<CreatePdfContainerCommand>
{
    public CreatePdfCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
