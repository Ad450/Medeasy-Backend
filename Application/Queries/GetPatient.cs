

using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetPatientQuery(GetPatientByIdDto dto) : IRequest<Patient>
{
    public GetPatientByIdDto Param { get; } = dto;
}

public class GetPatientHandler(IPatientService _patientService) : IRequestHandler<GetPatientQuery, Patient>
{
    public async Task<Patient> Handle(GetPatientQuery request, CancellationToken cancellationToken)
    {
        return await _patientService.GetPatientById(request.Param);
    }
}