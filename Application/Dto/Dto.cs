using Domain.Enum;

namespace Application.Dto;

public record AuthDto(string email, string password, IList<UserRole> Roles);

public record RefreshTokenDto(string accessToken, string refreshToken);
public record CreatePatientDto(
    string FirstName,
    string LastName,
    int Age,
    string ProfilePicUrl,
    string LocationName
 );


public record GetPatientByIdDto(Guid Id);
public record PaginationDto(string? searchTerm, int? pageSize, int? pageNumber, string? orderBy);

public record CreatePractitionerDto(
    string FirstName,
    string LastName,
    int Age,
    string ProfilePicUrl,
    string LocationName
);
public record GetPractitionerByIdDto(Guid Id);
public record GetPractitionerServicesDto(Guid Id);
public record UpdateLocationDto(
    Guid Id,
    string LocationName,
    double? Lat,
    double? Long
);

public record UpdateProfilePictureDto(
    Guid Id,
    string ProfilePicUrl
);


public record VerifyKycDto();


public record CreateAppointmentDto(
    Guid PatientId,
    Guid PractitionerId,
    Guid ServiceId,
    Guid DayId,
    string Name
);

public record GetAppointmentByIdDto(Guid Id);

public record GetAllPractitionerAppointmentsDto(Guid Id);

public record GetAllPatientAppointmentsDto(Guid Id);



public record UpdateAppointmentDto(
    Guid AppointmentId,
    Guid? PractitionerId,
    Guid? ServiceId,
    Guid? DayId,
    string? Name,
    string? Time
);

public record UpdateAppointmentStateDto
(
    Guid AppointmentId,
    AppointmentStatus status
);

public record CreateDaysDto(
    ICollection<CreateDayParam> Days
);
public record GetDaysDto(Guid PractitionerId);

public record CreateDayParam(
 DayOfWeek DayOfWeek,
 int WeekNumber,
 Guid PractitionerId
);


public record InitializeRolesDto(IList<string> Roles);

public record CreateServiceDto(string name);