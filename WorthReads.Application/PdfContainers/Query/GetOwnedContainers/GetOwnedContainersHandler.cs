using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.PdfContainers.Common;
using Mapster;
using MediatR;
using OneOf;
using WorthReads.Application.Common.Exceptions;
using WorthReads.Application.Common.Exceptions.ValidationException;
using WorthReads.Domain.Users;
using WorthReads.Domain.Users.ValueObjects;

namespace Application.PdfContainers.Query.GetOwnedContainers;

public class GetOwnedContainersHandler : IRequestHandler<GetOwnedContainersQuery, OneOf<List<PdfContainerResult>, IServiceError, ValidationErrors>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOwnedContainersHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OneOf<List<PdfContainerResult>, IServiceError, ValidationErrors>> Handle(GetOwnedContainersQuery request, CancellationToken cancellationToken)
    {
        //Get all the containers the user owns.
        //First get the user.
        var userId = UserId.Create(request.UserId);
        User user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
        if (user is null) return new UserNotFoundError();

        //Get the containerId of containers the user owns
        var containerIds = user.OwningPdfs;

        var containers = await _unitOfWork.PdfContainerRepository.GetPdfListAsync(containerIds);
        return containers.Adapt<List<PdfContainerResult>>();
    }
}
