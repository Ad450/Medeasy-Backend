using Application.Commands;
using Application.Dto;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Medeasy_Backend.Controllers;

public class PractitionerController : MedeasyBaseController
{
    [HttpPost]
    public async Task<ActionResult> CreatePractitioner([FromBody] CreatePractitionerDto body)
    {
        return new CreatedResult(location: nameof(CreatePractitioner), value: await Mediator.Send(new CreatePractitionerCommand(body)));
    }

    [HttpPut("location")]
    public async Task<ActionResult> UpdatePractitionerLocation([FromBody] UpdateLocationDto body)
    {
        return new AcceptedResult(nameof(UpdatePractitionerLocation), await Mediator.Send(new UpdatePractitionerLocationCommand(body)));
    }

    [HttpPut("profile")]
    public async Task<ActionResult> UpdatePractitionerProfilePicture([FromBody] UpdateProfilePictureDto body)
    {
        return new OkObjectResult(await Mediator.Send(new UpdatePractitionerProfileCommand(body)));
    }

    [HttpGet("all")]
    public async Task<ActionResult> GetAllPractitioners([FromQuery] GetAllPractitionersDto query)
    {
        return new OkObjectResult(await Mediator.Send(new GetAllPractitionersQuery(query)));
    }

    [HttpGet]
    public async Task<ActionResult> GetPractitioner([FromQuery] GetPractitionerByIdDto query)
    {
        return new OkObjectResult(await Mediator.Send(new GetPractitionerQuery(query)));
    }

    [HttpGet("all/appointments")]
    public async Task<ActionResult> GetAllPractitionerAppointments([FromQuery] GetAllPractitionerAppointmentsDto query)
    {
        return new OkObjectResult(await Mediator.Send(new GetAllPractitionerAppointmentsQuery(query)));
    }

}