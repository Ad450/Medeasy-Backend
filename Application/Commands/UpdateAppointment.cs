
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.Commands;

public class UpdateAppointmentCommand(UpdateAppointmentDto dto) : IRequest<Unit>
{
    public UpdateAppointmentDto Param { get; } = dto;
}


public class UpdateAppointmentHandler(IAppointmentService _appointmentService) : IRequestHandler<UpdateAppointmentCommand, Unit>
{
    public async Task<Unit> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        await _appointmentService.Update(request.Param);
        return Unit.Value;
    }
}