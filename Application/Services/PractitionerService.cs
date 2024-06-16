
using System.Linq.Expressions;
using Application.Dto;
using Application.Interfaces;
using Application.Utils;
using Domain.Entities;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class PractitionerService(
    IBaseRepository<Practitioner> _practitionerRepository,
    IBaseRepository<PractitionerLocation> _practitionerLocationRepository,
    IBaseRepository<PractitionerProfilePicture> _practitionerProfilePictureRepository
) : IPractitionerService
{
    public async Task<Guid> CreatePractitioner(CreatePractitionerDto dto)
    {
        var newPractitioner = new Practitioner
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Age = dto.Age,
        };
        if (dto.ProfilePicUrl != null)
        {
            var profilePic = new PractitionerProfilePicture { PictureUrl = dto.ProfilePicUrl, Practitioner = newPractitioner };
            await _practitionerProfilePictureRepository.Save(profilePic);
        }
        if (dto.LocationName != null)
        {
            var location = new PractitionerLocation { LocationName = dto.LocationName, Practitioner = newPractitioner };
            await _practitionerLocationRepository.Save(location);
        }
        return newPractitioner.Id;
    }

    public IList<Practitioner> GetAllPractitioners(PaginationDto dto)
    {
        return SearchUtil.FetchByPagination(
            repository: _practitionerRepository,
            searchFields: ["FirstName", "LastName"],
            dto: dto,
            orderBy: GetOrderby(dto.orderBy)
        );
    }
    private Expression<Func<Practitioner, object>> GetOrderby(string? orderBy)
    {
        return orderBy?.ToLower() switch
        {
            "age" => (p) => p.Age,
            "firstname" => (p) => p.FirstName,
            "lastname" => (p) => p.LastName,
            _ => (p) => p.Id,
        };

    }
    public async Task<Practitioner> GetPractitionerById(GetPractitionerByIdDto dto)
    {
        return await _practitionerRepository.GetById(dto.Id) ??
            throw new Exception("practitioner not found");
    }

    public async Task UpdateProfilePicture(UpdateProfilePictureDto dto)
    {
        var profilePicture = await _practitionerProfilePictureRepository.GetById(dto.Id)
             ?? throw new Exception("profile picture could not be retrieved");

        profilePicture.PictureUrl = dto.ProfilePicUrl;
        await _practitionerProfilePictureRepository.Update();
    }

    public async Task UpdatePractitionerLocation(UpdateLocationDto dto)
    {
        var location = await _practitionerLocationRepository.GetById(dto.Id)
              ?? throw new Exception("Location could not be retrieved");

        location.LocationName = dto.LocationName;
        if (dto.Lat != null) location.Lattitude = (double)dto.Lat!;
        if (dto.Long != null) location.Longitude = (double)dto.Long!;

        await _practitionerLocationRepository.Update();
    }

    public async Task<ICollection<Service>> GetPractitionerServices(GetPractitionerServicesDto dto)
    {
        var practitioner = await _practitionerRepository.GetById(dto.Id)
             ?? throw new Exception("practitioner not found");
        return practitioner.Services;
    }
}