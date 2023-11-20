using MapsterMapper;
using MediatR;
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

    public UsersController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
    {
        var createUserCommand = _mapper.Map<CreateUserCommand>(request);
        var createUserCommandResult = await _mediator.Send(createUserCommand);

        return createUserCommandResult.Match(
                userResponse => Ok(userResponse),
                serviceError => Problem(title: "Error", statusCode: serviceError.StatusCode, detail: serviceError.ErrorMessage),
                validationErrors => Problem(title: "Validation Error", statusCode: (int)HttpStatusCode.BadRequest, detail: validationErrors.GetValidationErrors())
            );
    }

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
}
