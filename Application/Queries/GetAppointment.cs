
using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetAppointmentQuery(GetAppointmentByIdDto dto) : IRequest<Appointment>
{
    public GetAppointmentByIdDto Param = dto;
}

public class GetAppointmentHandler(IAppointmentService _appointmentService) : IRequestHandler<GetAppointmentQuery, Appointment>
{
    public async Task<Appointment> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
    {
        return await _appointmentService.GetAppointmentById(request.Param);
    }
}