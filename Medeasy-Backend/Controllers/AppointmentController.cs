
using Application.Commands;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Medeasy_Backend.Controllers;

public class AppointmentController : MedeasyBaseController
{

    [HttpPut]
    public async Task<ActionResult> UpdateAppointment([FromBody] UpdateAppointmentDto body)
    {
        return new AcceptedResult(nameof(UpdateAppointment), await Mediator.Send(new UpdateAppointmentCommand(body)));
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAppointmentState([FromBody] UpdateAppointmentStateDto body)
    {
        return new OkObjectResult(await Mediator.Send(new UpdateAppointmentStateCommand(body)));
    }

}