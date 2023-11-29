using FluentValidation;

namespace Application.PdfContainers.Query.GetOwnedContainers;

public class GetOwnerContainersQueryValidator : AbstractValidator<GetOwnedContainersQuery>
{
    public GetOwnerContainersQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
