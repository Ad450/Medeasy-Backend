
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.Commands;

public class UpdatePractitionerLocationCommand(UpdateLocationDto dto) : IRequest<Unit>
{
    public UpdateLocationDto Param { get; } = dto;
}


public class UpdatePractitionerHandler(IPractitionerService _practitionerService) : IRequestHandler<UpdatePractitionerLocationCommand, Unit>
{
    public async Task<Unit> Handle(UpdatePractitionerLocationCommand request, CancellationToken cancellationToken)
    {
        await _practitionerService.UpdatePractitionerLocation(request.Param);
        return Unit.Value;
    }
}