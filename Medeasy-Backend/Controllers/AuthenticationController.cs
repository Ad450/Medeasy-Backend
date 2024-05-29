using Application.Commands;
using Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medeasy_Backend.Controllers;

public class AuthenticationController : MedeasyBaseController
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] AuthDto body)
    {
        return new CreatedResult(location: nameof(RegisterUser), value: await Mediator.Send(new RegisterUserCommand(body)));
    }

    [AllowAnonymous]
    [HttpPost("roles")]
    public async Task<ActionResult> InitializeRoles([FromBody] InitializeRolesDto body)
    {
        return new CreatedResult(location: nameof(InitializeRoles), value: await Mediator.Send(new InitializeRolesCommand(body)));
    }

    [AllowAnonymous]
    [HttpPost("signin")]
    public async Task<ActionResult> Signin([FromBody] AuthDto body)
    {
        return new OkObjectResult(await Mediator.Send(new SigninUserCommand(body)));
    }

}