using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetAllPractitionersQuery(PaginationDto dto) : IRequest<IList<Practitioner>>
{
    public PaginationDto Param { get; } = dto;
}

public class GetAllPractitionersHandler(IPractitionerService _practitionerService) : IRequestHandler<GetAllPractitionersQuery, IList<Practitioner>>
{
    public async Task<IList<Practitioner>> Handle(GetAllPractitionersQuery request, CancellationToken cancellationToken)
    {
        return _practitionerService.GetAllPractitioners(request.Param);
    }
}