using Application.Dto;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPractitionerService
{
    Task<Guid> CreatePractitioner(CreatePractitionerDto dto);
    Task<Practitioner> GetPractitionerById(GetPractitionerByIdDto dto);
    IList<Practitioner> GetAllPractitioners(GetAllPractitionersDto dto);

    Task UpdateProfilePicture(UpdateProfilePictureDto dto);
    Task UpdatePractitionerLocation(UpdateLocationDto dto);

    Task<ICollection<Service>> GetPractitionerServices(GetPractitionerServicesDto dto);
}