
using Application.Commands;
using Application.Dto;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Medeasy_Backend.Controllers;

public class PatientController : MedeasyBaseController
{
    [HttpPost]
    public async Task<ActionResult> CreatePatient([FromBody] CreatePatientDto body)
    {
        return new CreatedResult(location: nameof(CreatePatient), value: await Mediator.Send(new CreatePatientCommand(body)));
    }
    [HttpPut("location")]
    public async Task<ActionResult> UpdatePatientLocation([FromBody] UpdateLocationDto body)
    {
        return new AcceptedResult(nameof(UpdatePatientLocation), await Mediator.Send(new UpdatePatientLocationCommand(body)));
    }

    [HttpPut("profile")]
    public async Task<ActionResult> UpdatePatientProfilePicture([FromBody] UpdateProfilePictureDto body)
    {
        return new OkObjectResult(await Mediator.Send(new UpdatePatientProfileCommand(body)));
    }

    [HttpGet("all")]
    public async Task<ActionResult> GetAllPatients([FromQuery] GetAllPatientsDto query)
    {
        return new OkObjectResult(await Mediator.Send(new GetAllPatientsQuery(query)));
    }

    [HttpGet]
    public async Task<ActionResult> GetPatient([FromQuery] GetPatientByIdDto query)
    {
        return new OkObjectResult(await Mediator.Send(new GetPatientQuery(query)));
    }

    [HttpGet("all/appointments")]
    public async Task<ActionResult> GetAllPatientAppointments([FromQuery] GetAllPatientAppointmentsDto query)
    {
        return new OkObjectResult(await Mediator.Send(new GetAllPatientAppointmentsQuery(query)));
    }


}