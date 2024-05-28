using Application.Commands;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Medeasy_Backend.Controllers;

public class AuthenticationController : MedeasyBaseController
{
    [HttpPost]
    public async Task<ActionResult> RegisterUser([FromBody] AuthDto body)
    {
        return new CreatedResult(location: nameof(RegisterUser), value: await Mediator.Send(new RegisterUserCommand(body)));
    }

    [HttpPost]
    public async Task<ActionResult> Signin([FromQuery] AuthDto body)
    {
        return new OkObjectResult(await Mediator.Send(new SigninUserCommand(body)));
    }

}