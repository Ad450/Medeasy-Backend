using Application.Dto;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPatientService
{
    public Task<Guid> CreatePatient(CreatePatientDto dto);
    public Task<Patient> GetPatientById(GetPatientByIdDto dto);
    public IList<Patient> GetAllPatients(PaginationDto dto);

    public Task UpdateProfilePicture(UpdateProfilePictureDto dto);
    public Task UpdatePatientLocation(UpdateLocationDto dto);
}