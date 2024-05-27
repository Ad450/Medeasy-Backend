using Application.Dto;
using Domain.Entities;

namespace Application.Interfaces;

public interface IAppointmentService
{
    public Task<Guid> Create(CreateAppointmentDto dto);
    public Task Update(UpdateAppointmentDto dto);

    public Task UpdateAppointmentState(UpdateAppointmentStateDto dto);
    public IList<Appointment> GetAllPractitionerAppointments(GetAllPractitionerAppointmentsDto dto);
    public IList<Appointment> GetAllPatientAppointments(GetAllPatientAppointmentsDto dto);
    public Task<Appointment> GetAppointmentById(GetAppointmentByIdDto dto);
}