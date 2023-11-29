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
        //Creating a new Container.
        var container = PdfContainer.Create(UserId.Create(request.UserId), request.Name);

        //Save Container
        await _unitOfWork.PdfContainerRepository.AddAsync(container);

        var result = _mapper.Map<PdfContainerResult>(container);
        return result;
    }
}
