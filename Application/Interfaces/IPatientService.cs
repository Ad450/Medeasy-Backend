using Application.Dto;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPatientService
{
    Task CreatePatient(CreatePatientDto dto);
    Task<Patient> GetPatientById(GetPatientByIdDto dto);
    IList<Patient> GetAllPatients(GetAllPatientsDto dto);
}