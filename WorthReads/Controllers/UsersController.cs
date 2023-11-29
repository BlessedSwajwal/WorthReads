using Infrastructure.Services;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WorthReads.Application.Users.Commands.CreateUser;
using WorthReads.Application.Users.Queries.Login;
using WorthReads.Contracts;

namespace WorthReads.API.Controllers;

[Route("Users")]
public class UsersController : Controller
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly IPocketAPIService _pocketAPIService;

    public UsersController(ISender mediator, IMapper mapper, IPocketAPIService pocketAPIService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _pocketAPIService = pocketAPIService;
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
    {
        var createUserCommand = _mapper.Map<CreateUserCommand>(request);
        var createUserCommandResult = await _mediator.Send(createUserCommand);

        var ctx = HttpContext.User;
        var res = createUserCommandResult.Match(
                userResponse => Ok(userResponse),
                serviceError => Problem(title: "Error", statusCode: serviceError.StatusCode, detail: serviceError.ErrorMessage),
                validationErrors => Problem(title: "Validation Error", statusCode: (int)HttpStatusCode.BadRequest, detail: validationErrors.GetValidationErrors())
            );
        return res;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var loginQuery = _mapper.Map<LoginQuery>(request);
        var loginQueryResult = await _mediator.Send(loginQuery);

        return loginQueryResult.Match(
                userResponse => Ok(userResponse),
                serviceError => Problem(title: "Error", statusCode: serviceError.StatusCode, detail: serviceError.ErrorMessage),
                ruleValidationErrors => Problem(title: "Error", statusCode: (int)HttpStatusCode.BadRequest, detail: ruleValidationErrors.GetValidationErrors()));
    }

    [AllowAnonymous]
    [HttpGet("Test")]
    public async Task<IActionResult> Test()
    {
        var res = await _pocketAPIService.GetPocketList();
        return Ok(res);
    }

}
