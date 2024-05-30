using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medeasy_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedeasyBaseController : ControllerBase
{
    private protected IMediator Mediator
    {
        get
        {
            var mediator = HttpContext.RequestServices.GetService<IMediator>()
                ?? throw new Exception("Could not fetch mediator from DI Container");
            return mediator;
        }
    }
}