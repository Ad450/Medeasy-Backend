using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.Commands;

public class CreatePractitionerCommand(CreatePractitionerDto dto) : IRequest<Guid>
{
    public CreatePractitionerDto Param = dto;
}


public class CreatePractitionerHandler(IPractitionerService _practitionerService) : IRequestHandler<CreatePractitionerCommand, Guid>
{
    public async Task<Guid> Handle(CreatePractitionerCommand request, CancellationToken cancellationToken)
    {
        return await _practitionerService.CreatePractitioner(request.Param);
    }
}