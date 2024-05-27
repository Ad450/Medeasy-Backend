
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.Commands;

public class CreatePatientCommand(CreatePatientDto dto) : IRequest<Guid>
{
    public CreatePatientDto Param = dto;
}


public class CreatePatientHandler(IPatientService _patientService) : IRequestHandler<CreatePatientCommand, Guid>
{
    public async Task<Guid> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        return await _patientService.CreatePatient(request.Param);
    }
}