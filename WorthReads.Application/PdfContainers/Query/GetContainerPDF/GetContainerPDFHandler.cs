using Application.Common.Authorization;
using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Services;
using Domain.PdfContainer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using OneOf;
using WorthReads.Application.Common.Exceptions;
using WorthReads.Application.Common.Exceptions.ValidationException;

namespace Application.PdfContainers.Query.GetContainerPDF;

public class GetContainerPDFHandler : IRequestHandler<GetContainerPDFQuery, OneOf<PDFResponse, IServiceError, ValidationErrors>>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenerateContainerPdf _pdfGeneratorService;

    public GetContainerPDFHandler(IAuthorizationService authorizationService, IUnitOfWork unitOfWork, IGenerateContainerPdf pdfGeneratorService)
    {
        _authorizationService = authorizationService;
        _unitOfWork = unitOfWork;
        _pdfGeneratorService = pdfGeneratorService;
    }

    public async Task<OneOf<PDFResponse, IServiceError, ValidationErrors>> Handle(GetContainerPDFQuery request, CancellationToken cancellationToken)
    {
        //Get the said container.
        var containerId = PdfContainerId.Create(request.ContainerId);
        var container = await _unitOfWork.PdfContainerRepository.GetFromIdAsync(containerId);
        if (container == PdfContainer.ContainerEmpty) return new ContainerNotFoundError();

        //Validate the user owns the container.
        var authResult = await _authorizationService.AuthorizeAsync(request.User, container, PdfContainerOperations.GenerateContainerPDF);

        if (!authResult.Succeeded)
        {
            return new ContainerNotOwnedError();
        }

        var reads = await _unitOfWork.ReadsRepository.GetPdfsFromUrlListAsync(container.ReadsUrl);
        byte[] pdf = await _pdfGeneratorService.GenerateContainerPdf(reads);
        return new PDFResponse(pdf, container.Name);
    }
}
