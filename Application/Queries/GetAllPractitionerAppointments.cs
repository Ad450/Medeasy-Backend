
using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetAllPractitionerAppointmentsQuery(GetAllPractitionerAppointmentsDto dto) : IRequest<IList<Appointment>>
{
    public GetAllPractitionerAppointmentsDto Param { get; } = dto;
}

public class GetAllPractitionerAppointmentsHandler(IAppointmentService _appointmentService) : IRequestHandler<GetAllPractitionerAppointmentsQuery, IList<Appointment>>
{
    public async Task<IList<Appointment>> Handle(GetAllPractitionerAppointmentsQuery request, CancellationToken cancellationToken)
    {
        return _appointmentService.GetAllPractitionerAppointments(request.Param);
    }
}

