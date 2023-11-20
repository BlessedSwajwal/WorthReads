using Application.Common.Interfaces.Repositories;
using Domain.Reads;
using MediatR;
using OneOf;
using WorthReads.Application.Common.Exceptions;
using WorthReads.Application.Common.Exceptions.ValidationException;

namespace Application.Reads.Query;

public class GetReadsQueryHandler : IRequestHandler<GetReadsQuery, OneOf<List<Domain.Reads.Reads>, IServiceError, ValidationErrors>>
{
    private readonly IReadsRepository _readsRepository;

    public GetReadsQueryHandler(IReadsRepository readsRepository)
    {
        _readsRepository = readsRepository;
    }

    public async Task<OneOf<List<Domain.Reads.Reads>, IServiceError, ValidationErrors>> Handle(GetReadsQuery request, CancellationToken cancellationToken)
    {
        ReadsStatus status = request.Status.ToLower() switch
        {
            "popular" => ReadsStatus.popular,
            "recent" => ReadsStatus.recent,
            _ => ReadsStatus.popular
        };

        var reads = status switch
        {
            ReadsStatus.popular => _readsRepository.GetPopularReads(),
            ReadsStatus.recent => _readsRepository.GetRecentReads(),
            _ => _readsRepository.GetPopularReads()
        };

        return reads;
    }
}

