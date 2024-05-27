using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetPractitionerQuery(GetPractitionerByIdDto dto) : IRequest<Practitioner>
{
    public GetPractitionerByIdDto Param { get; } = dto;
}

public class GetPractitionerHandler(IPractitionerService _practitionerService) : IRequestHandler<GetPractitionerQuery, Practitioner>
{
    public async Task<Practitioner> Handle(GetPractitionerQuery request, CancellationToken cancellationToken)
    {
        return await _practitionerService.GetPractitionerById(request.Param);
    }
}