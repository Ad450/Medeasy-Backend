
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.Commands;

public class UpdateAppointmentStateCommand(UpdateAppointmentStateDto dto) : IRequest<Unit>
{
    public UpdateAppointmentStateDto Param = dto;
}


public class UpdateAppointmentStateHandler(IAppointmentService _appointmentService) : IRequestHandler<UpdateAppointmentStateCommand, Unit>
{
    public async Task<Unit> Handle(UpdateAppointmentStateCommand request, CancellationToken cancellationToken)
    {
        await _appointmentService.UpdateAppointmentState(request.Param);
        return Unit.Value;
    }
}