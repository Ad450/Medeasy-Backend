using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.Commands;

public class UpdatePractitionerProfileCommand(UpdateProfilePictureDto dto) : IRequest<Unit>
{
    public UpdateProfilePictureDto Param = dto;
}


public class UpdatePractitionerProfileHandler(IPractitionerService _practitionerService) : IRequestHandler<UpdatePractitionerProfileCommand, Unit>
{
    public async Task<Unit> Handle(UpdatePractitionerProfileCommand request, CancellationToken cancellationToken)
    {
        await _practitionerService.UpdateProfilePicture(request.Param);
        return Unit.Value;
    }
}