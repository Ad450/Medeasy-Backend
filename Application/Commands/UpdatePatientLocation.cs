
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.Commands;

public class UpdatePatientLocationCommand(UpdateLocationDto dto) : IRequest<Unit>
{
    public UpdateLocationDto Param { get; } = dto;
}


public class UpdatePatientLocationHandler(IPatientService _patientService) : IRequestHandler<UpdatePatientLocationCommand, Unit>
{
    public async Task<Unit> Handle(UpdatePatientLocationCommand request, CancellationToken cancellationToken)
    {
        await _patientService.UpdatePatientLocation(request.Param);
        return Unit.Value;
    }
}