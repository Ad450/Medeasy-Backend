using Application.Dto;

namespace Application.Interfaces;

public interface IAppointmentService
{
    Task<Guid> Create(CreateAppointmentDto dto);
    Task Update(UpdateAppointmentDto dto);

    Task UpdateAppointmentState(UpdateAppointmentStateDto dto);
}