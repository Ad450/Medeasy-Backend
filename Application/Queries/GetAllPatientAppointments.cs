
using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetAllPatientAppointmentsQuery(GetAllPatientAppointmentsDto dto) : IRequest<IList<Appointment>>
{
    public GetAllPatientAppointmentsDto Param = dto;
}

public class GetAllPatientAppointmentsHandler(IAppointmentService _appointmentService) : IRequestHandler<GetAllPatientAppointmentsQuery, IList<Appointment>>
{
    public async Task<IList<Appointment>> Handle(GetAllPatientAppointmentsQuery request, CancellationToken cancellationToken)
    {
        return _appointmentService.GetAllPatientAppointments(request.Param);
    }
}