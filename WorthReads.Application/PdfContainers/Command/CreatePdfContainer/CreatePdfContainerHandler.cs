using Application.Common.Interfaces.Repositories;
using Application.PdfContainers.Common;
using Domain.PdfContainer;
using MapsterMapper;
using MediatR;
using OneOf;
using WorthReads.Application.Common.Exceptions;
using WorthReads.Application.Common.Exceptions.ValidationException;
using WorthReads.Domain.Users.ValueObjects;

namespace Application.PdfContainers.Command.CreatePdfContainer;

public class CreatePdfContainerHandler : IRequestHandler<CreatePdfContainerCommand, OneOf<PdfContainerResult, IServiceError, ValidationErrors>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePdfContainerHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OneOf<PdfContainerResult, IServiceError, ValidationErrors>> Handle(CreatePdfContainerCommand request, CancellationToken cancellationToken)
    {
        var userId = UserId.Create(request.UserId);
        //Creating a new Container.
        var container = PdfContainer.Create(userId, request.Name);

        //Save Container
        await _unitOfWork.PdfContainerRepository.AddAsync(container);
        //TODO - Use events to add containers to Users list
        var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
        user.AddPdfContainer(container.Id);

        var result = _mapper.Map<PdfContainerResult>(container);
        return result;
    }
}
