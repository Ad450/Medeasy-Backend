using System.Linq.Expressions;
using Application.Dto;
using Application.Interfaces;
using Application.Utils;
using Domain.Entities;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class PatientService(
    IBaseRepository<Patient> _patientRepository,
    IBaseRepository<PatientProfilePicture> _patientProfileRepository,
    IBaseRepository<PatientLocation> _patientLocationRepository
) : IPatientService
{
    public async Task<Guid> CreatePatient(CreatePatientDto dto)
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
        return newPatient.Id;

    }

    public IList<Patient> GetAllPatients(PaginationDto dto)
    {

        return SearchUtil.FetchByPagination(
            repository: _patientRepository,
            searchFields: ["FirstName", "LastName"],
            dto: dto,
            orderBy: GetOrderby(dto.orderBy)
        );
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
        return await _patientRepository.GetById(dto.Id) ??
            throw new Exception("practitioner not found");
    }

    public async Task UpdateProfilePicture(UpdateProfilePictureDto dto)
    {
        var profilePicture = await _patientProfileRepository.GetById(dto.Id)
             ?? throw new Exception("profile picture could not be retrieved");

        profilePicture.PictureUrl = dto.ProfilePicUrl;
        await _patientProfileRepository.Update();
    }

    public async Task UpdatePatientLocation(UpdateLocationDto dto)
    {
        var location = await _patientLocationRepository.GetById(dto.Id)
              ?? throw new Exception("Location could not be retrieved");

        location.LocationName = dto.LocationName;
        if (dto.Lat != null) location.Lattitude = (double)dto.Lat!;
        if (dto.Long != null) location.Longitude = (double)dto.Long!;

        await _patientProfileRepository.Update();
    }
}