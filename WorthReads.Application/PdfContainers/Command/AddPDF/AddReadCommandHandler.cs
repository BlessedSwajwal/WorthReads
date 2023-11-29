using Application.Common.Authorization;
using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Domain.PdfContainer;
using Domain.Reads;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using OneOf;
using OneOf.Types;
using WorthReads.Application.Common.Exceptions;
using WorthReads.Application.Common.Exceptions.ValidationException;

namespace Application.PdfContainers.Command.AddPDF;

public class AddReadCommandHandler : IRequestHandler<AddReadCommand, OneOf<Some, IServiceError, ValidationErrors>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorizationService _authorizationService;

    public AddReadCommandHandler(IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
    {
        _unitOfWork = unitOfWork;
        _authorizationService = authorizationService;
    }

    public async Task<OneOf<Some, IServiceError, ValidationErrors>> Handle(AddReadCommand request, CancellationToken cancellationToken)
    {

        //Get the container
        PdfContainer container = await _unitOfWork.PdfContainerRepository.GetFromIdAsync(PdfContainerId.Create(request.ContainerId));

        //Verify the user is owner of the container.
        var authResult = await _authorizationService.AuthorizeAsync(request.User, container, PdfContainerOperations.AddPdf);

        if (!authResult.Succeeded)
        {
            return new ContainerNotOwnedError();
        }

        //Get the pdf
        Read pdf = await _unitOfWork.ReadsRepository.GetPdfDetailsAsync(request.url);

        //TODO - If pdfDetail is not found, get the pdf and save it to database.
        if (pdf == Read.EmptyReads)
        {
            throw new Exception("pdf not found");
        }

        if (container.ReadsUrl.Contains(request.url))
        {
            return new Some();
        }
        container.AddReadsUrl(request.url);
        return new Some();
    }
}
