
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.Commands;

public class UpdatePatientProfileCommand(UpdateProfilePictureDto dto) : IRequest<Unit>
{
    public UpdateProfilePictureDto Param = dto;
}


public class UpdatePatientProfileHandler(IPatientService _patientService) : IRequestHandler<UpdatePatientProfileCommand, Unit>
{
    public async Task<Unit> Handle(UpdatePatientProfileCommand request, CancellationToken cancellationToken)
    {
        await _patientService.UpdateProfilePicture(request.Param);
        return Unit.Value;
    }
}