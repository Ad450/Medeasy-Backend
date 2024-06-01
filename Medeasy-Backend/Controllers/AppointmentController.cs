
using Application.Commands;
using Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medeasy_Backend.Controllers;

[Authorize(Policy = "PatientORPractitioner")]
public class AppointmentController() : MedeasyBaseController
{

    [HttpPost(), Authorize(Roles = "Patient")]
    public async Task<ActionResult> CreateAppointment([FromBody] CreateAppointmentDto body)
    {
        return new CreatedResult(nameof(CreateAppointment), await Mediator.Send(new CreateAppointmentCommand(body)));
    }
    [HttpPut()]
    public async Task<ActionResult> UpdateAppointment([FromBody] UpdateAppointmentDto body)
    {
        return new AcceptedResult(nameof(UpdateAppointment), await Mediator.Send(new UpdateAppointmentCommand(body)));
    }

    [HttpPut("state")]
    public async Task<ActionResult> UpdateAppointmentState([FromBody] UpdateAppointmentStateDto body)
    {
        return new OkObjectResult(await Mediator.Send(new UpdateAppointmentStateCommand(body)));
    }

}