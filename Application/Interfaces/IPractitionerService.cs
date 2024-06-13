using Application.Dto;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPractitionerService
{
    public Task<Guid> CreatePractitioner(CreatePractitionerDto dto);
    public Task<Practitioner> GetPractitionerById(GetPractitionerByIdDto dto);
    public IList<Practitioner> GetAllPractitioners(PaginationDto dto);

    public Task UpdateProfilePicture(UpdateProfilePictureDto dto);
    public Task UpdatePractitionerLocation(UpdateLocationDto dto);

    public Task<ICollection<Service>> GetPractitionerServices(GetPractitionerServicesDto dto);
}