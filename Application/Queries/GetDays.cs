using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetDaysQuery(GetDaysDto dto) : IRequest<ICollection<Day>>
{
    public GetDaysDto Param = dto;
}

public class GeDaysHandler(IDayService _dayService) : IRequestHandler<GetDaysQuery, ICollection<Day>>
{
    public async Task<ICollection<Day>> Handle(GetDaysQuery request, CancellationToken cancellationToken)
    {
        return _dayService.GetDays(request.Param);
    }
}