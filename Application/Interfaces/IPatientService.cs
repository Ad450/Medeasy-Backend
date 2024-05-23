using Application.Dto;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPatientService
{
    Task<Guid> CreatePatient(CreatePatientDto dto);
    Task<Patient> GetPatientById(GetPatientByIdDto dto);
    IList<Patient> GetAllPatients(GetAllPatientsDto dto);
}