
using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Repository;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;


namespace Application.Services;

public class AppointmentService(
    IBaseRepository<Appointment> _appointmentRepository,
     IBaseRepository<Patient> _patientRepository,
    IBaseRepository<Practitioner> _practitionerRepository,
    IBaseRepository<Day> _dayRepository,
    IBaseRepository<Service> _serviceRepository,
    IBaseRepository<AppointmentState> _appointmentStateRepository
    ) : IAppointmentService
{
    public async Task<Guid> Create(CreateAppointmentDto dto)
    {
        using var transaction = await _appointmentRepository.GetContext().Database.BeginTransactionAsync();
        try
        {

            var patient = await _patientRepository.GetById(dto.PatientId)
                ?? throw new Exception("patient not found");
            var practioner = await _practitionerRepository.GetById(dto.PractitionerId)
                ?? throw new Exception("practitioner not found");
            var day = await _dayRepository.GetById(dto.DayId)
                ?? throw new Exception("day not found");
            var service = await _serviceRepository.GetById(dto.ServiceId)
                ?? throw new Exception("service not found");

            var appointment = new Appointment
            {
                Name = dto.Name,
                Patient = patient,
                Practitioner = practioner,
                Day = day,
                Service = service,
                AppointmentState = new AppointmentState { AppointmentStatus = AppointmentStatus.CREATED }
            };

            await _appointmentRepository.Save(appointment);
            await transaction.CommitAsync();
            return appointment.Id;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new Exception($"Exception: {e} while creating appointment");
        }
        finally
        {
            await transaction.DisposeAsync();
        }
    }

    public async Task Update(UpdateAppointmentDto dto)
    {
        var appointment = await _appointmentRepository.GetById(dto.AppointmentId)
            ?? throw new Exception("appointment not found");

        if (dto.DayId != null)
        {
            appointment.Day = await _dayRepository.GetById((Guid)dto.DayId);
        }
        if (dto.ServiceId != null)
        {
            appointment.Service = await _serviceRepository.GetById((Guid)dto.ServiceId);
        }
        if (dto.PractitionerId != null)
        {
            appointment.Practitioner = await _practitionerRepository.GetById((Guid)dto.PractitionerId);
        }

        appointment.Name = dto.Name ?? appointment.Name;

        await _appointmentRepository.Update();
    }

    public async Task UpdateAppointmentState(UpdateAppointmentStateDto dto)
    {
        try
        {

            var appointment = await _appointmentRepository.GetById(dto.AppointmentId)
                ?? throw new Exception("appointment not found");
            var appointmentState = await _appointmentStateRepository.GetByCondition((a) => a.AppointmentId == dto.AppointmentId)
                .FirstOrDefaultAsync() ?? throw new Exception("appointment state not found");

            appointmentState.AppointmentStatus = dto.status;
            await _appointmentStateRepository.Update();
        }
        catch (Exception e)
        {
            throw new Exception($"Exception {e} occured while trying to updtae appointment state");
        }
    }
}

