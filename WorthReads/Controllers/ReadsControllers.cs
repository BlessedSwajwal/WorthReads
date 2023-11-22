using Application.Reads.Query;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[Route("Reads")]
public class ReadsControllers : Controller
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ReadsControllers(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetReads([FromQuery] string status)
    {
        if (status == null) status = "popular";
        var getReadsQuery = new GetReadsQuery(status);
        var getReadsQueryResult = await _mediator.Send(getReadsQuery);

        return getReadsQueryResult.Match(
                readsResponse => Ok(readsResponse),
                serviceError => Problem(title: "Error", statusCode: serviceError.StatusCode, detail: serviceError.ErrorMessage),
                validationErrors => Problem(title: "Validation Error", statusCode: (int)HttpStatusCode.BadRequest, detail: validationErrors.GetValidationErrors())
            );
    }
}
