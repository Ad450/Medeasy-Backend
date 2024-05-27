
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetAllAppointmentsQuery : IRequest<IList<Appointment>>;

public class GetAllAppointmentsHandler(IAppointmentService _appointmentService) : IRequestHandler<GetAllAppointmentsQuery, IList<Appointment>>
{
    public async Task<IList<Appointment>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
    {
        return _appointmentService.GetAll();
    }
}