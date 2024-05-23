
using System.Linq.Expressions;
using Application.Dto;
using Application.Interfaces;
using Application.Utils;
using Domain.Entities;
using Infrastructure.Repository;

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

    public IList<Practitioner> GetAllPractitioners(GetAllPractitionersDto dto)
    {
        if (dto.searchTerm == null && dto.pageNumber == null)
            throw new Exception("either search or provide page numner");

        IQueryable<Practitioner> query = dto.searchTerm != null ?
            _practitionerRepository.GetByCondition(
                SearchUtil.BuildSearchExpression<Practitioner>(
                    dto.searchTerm, ["FirstName", "Age", "LastName"]
                )) : _practitionerRepository.GetAll();


        if (dto.pageNumber != null && dto.pageNumber != null)
        {
            var skip = (dto.pageNumber - 1) * dto.pageSize;
            query = query
                        .Skip((int)skip!)
                        .Take((int)dto.pageSize!);
        }

        return [.. query.OrderBy(GetOrderby(dto.orderBy))];
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
        return await _practitionerRepository.GetById(dto.Id);
    }
}