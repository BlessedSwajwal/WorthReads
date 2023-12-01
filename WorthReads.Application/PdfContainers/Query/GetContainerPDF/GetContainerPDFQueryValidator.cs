using FluentValidation;

namespace Application.PdfContainers.Query.GetContainerPDF;

public class GetContainerPDFQueryValidator : AbstractValidator<GetContainerPDFQuery>
{
    public GetContainerPDFQueryValidator()
    {
        RuleFor(c => c.ContainerId).NotEmpty();
    }
}
