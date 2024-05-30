using Application.Commands;
using Application.Dto;
using Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medeasy_Backend.Controllers;
[Authorize(Roles = "PatientORPractitioner")]
public class DayController : MedeasyBaseController
{
    [HttpPost]
    public async Task<ActionResult> CreateDays([FromBody] CreateDaysDto body)
    {
        return new CreatedResult(location: nameof(CreateDays), value: await Mediator.Send(new CreateDaysCommand(body)));
    }

    [HttpGet]
    public async Task<ActionResult> GetDays([FromQuery] GetDaysDto query)
    {
        return new OkObjectResult(await Mediator.Send(new GetDaysQuery(query)));
    }

}