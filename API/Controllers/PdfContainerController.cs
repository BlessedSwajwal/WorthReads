using Application.PdfContainers.Command.AddPDF;
using Application.PdfContainers.Command.CreatePdfContainer;
using Application.PdfContainers.Query.GetOwnedContainers;
using Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace API.Controllers;

[Route("PdfContainer")]
public class PdfContainerController : ControllerBase
{
    private readonly ISender _mediator;

    public PdfContainerController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateContainer([FromBody] CreatePdfContainerRequest request)
    {
        var user = HttpContext.User;
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        var command = new CreatePdfContainerCommand(Guid.Parse(userId), request.Name);

        var result = await _mediator.Send(command);
        var res = result.Match(
                containerResponse => Ok(containerResponse),
                serviceError => Problem(title: "Error", statusCode: serviceError.StatusCode, detail: serviceError.ErrorMessage),
                validationErrors => Problem(title: "Validation Error", statusCode: (int)HttpStatusCode.BadRequest, detail: validationErrors.GetValidationErrors())
        );
        return res;
    }

    [HttpPost("AddPdf")]
    public async Task<IActionResult> AddPdfToContainer([FromBody] AddPdfToContainerRequest request)
    {
        var user = HttpContext.User;
        var command = new AddReadCommand(user, request.ContainerId, request.Url);

        var result = await _mediator.Send(command);
        var res = result.Match(
                some => Ok("Done"),
                serviceError => Problem(title: "Error", statusCode: serviceError.StatusCode, detail: serviceError.ErrorMessage),
                validationErrors => Problem(title: "Validation Error", statusCode: (int)HttpStatusCode.BadRequest, detail: validationErrors.GetValidationErrors())
        );
        return res;
    }

    [HttpGet("OwnedContainers")]
    public async Task<IActionResult> GetOwnedContainer()
    {
        var userId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var query = new GetOwnedContainersQuery(userId);
        var result = await _mediator.Send(query);
        var res = result.Match(
                containers => Ok(containers),
                serviceError => Problem(title: "Error", statusCode: serviceError.StatusCode, detail: serviceError.ErrorMessage),
                validationErrors => Problem(title: "Validation Error", statusCode: (int)HttpStatusCode.BadRequest, detail: validationErrors.GetValidationErrors())
        );
        return res;
    }
}
