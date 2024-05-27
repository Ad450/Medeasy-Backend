
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.Commands;

public class CreateAppointmentCommand(CreateAppointmentDto dto) : IRequest<Guid>
{
    public CreateAppointmentDto Param = dto;
}


public class CreateAppointmentHandler(IAppointmentService _appointmentService) : IRequestHandler<CreateAppointmentCommand, Guid>
{
    public async Task<Guid> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        return await _appointmentService.Create(request.Param);
    }
}