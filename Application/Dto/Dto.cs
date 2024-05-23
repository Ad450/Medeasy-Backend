namespace Application.Dto;

public record AuthDto(string email, string password, IList<string> roles);
public record CreatePatientDto(
    string FirstName,
    string LastName,
    int Age,
    string ProfilePicUrl,
    string LocationName
 );
public record GetPatientByIdDto(Guid Id);
public record GetAllPatientsDto(string? searchTerm, int? pageSize, int? pageNumber, string orderBy);