

using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetAllPatientsQuery(PaginationDto dto) : IRequest<IList<Patient>>
{
    public PaginationDto Param { get; } = dto;
}

public class GetAllPatientsHandler(IPatientService _patientService) : IRequestHandler<GetAllPatientsQuery, IList<Patient>>
{
    public async Task<IList<Patient>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
    {
        return _patientService.GetAllPatients(request.Param);
    }
}