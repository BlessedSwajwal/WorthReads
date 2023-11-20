using FluentValidation;

namespace Application.Reads.Query;

public class GetReadsQueryValidator : AbstractValidator<GetReadsQuery>
{
    public GetReadsQueryValidator()
    {
        RuleFor(x => x.Status).NotEmpty();
    }
}
