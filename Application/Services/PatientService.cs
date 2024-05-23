using System.Data.Common;
using System.Linq.Expressions;
using Application.Dto;
using Application.Interfaces;
using Application.Utils;
using Domain.Entities;
using Infrastructure.Repository;

namespace Application.Services;

public class PatientService(
    IBaseRepository<Patient> _patientRepository,
    IBaseRepository<PatientProfilePicture> _patientProfileRepository,
    IBaseRepository<PatientLocation> _patientLocationRepository
) : IPatientService
{
    public async Task CreatePatient(CreatePatientDto dto)
    {
        var newPatient = new Patient
        {
            Age = dto.Age,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
        };

        await _patientRepository.Save(newPatient);

        if (dto.ProfilePicUrl != null)
        {
            var profilePic = new PatientProfilePicture { PictureUrl = dto.ProfilePicUrl, Patient = newPatient };
            await _patientProfileRepository.Save(profilePic);
        }
        if (dto.LocationName != null)
        {
            var patientLocation = new PatientLocation { LocationName = dto.LocationName, Patient = newPatient };
            await _patientLocationRepository.Save(patientLocation);
        }
        return;

    }

    public IList<Patient> GetAllPatients(GetAllPatientsDto dto)
    {

        if (dto.searchTerm == null && dto.pageNumber == null)
            throw new Exception("either search or provide page numner");

        var query = dto.searchTerm != null ?
            _patientRepository.GetByCondition(
                SearchUtil.BuildSearchExpression<Patient>(
                    dto.searchTerm, ["FirstName", "Age", "LastName"]
                )) : _patientRepository.GetAll();


        if (dto.pageNumber != null && dto.pageNumber != null)
        {
            var skip = (dto.pageNumber - 1) * dto.pageSize;
            query = query
                        .Skip((int)skip!)
                        .Take((int)dto.pageSize!);
        }

        return [.. query.OrderBy(GetOrderby(dto.orderBy))];
    }
    private Expression<Func<Patient, object>> GetOrderby(string? orderBy)
    {
        return orderBy?.ToLower() switch
        {
            "age" => (p) => p.Age,
            "firstname" => (p) => p.FirstName,
            "lastname" => (p) => p.LastName,
            _ => (p) => p.Id,
        };

    }
    public async Task<Patient> GetPatientById(GetPatientByIdDto dto)
    {
        return await _patientRepository.GetById(dto.Id);
    }
}