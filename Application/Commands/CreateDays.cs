
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.Commands;

public class CreateDaysCommand(CreateDaysDto dto) : IRequest<Unit>
{
    public CreateDaysDto Param = dto;
}


public class CreateDaysHandler(IDayService _dayService) : IRequestHandler<CreateDaysCommand, Unit>
{
    public async Task<Unit> Handle(CreateDaysCommand request, CancellationToken cancellationToken)
    {
        await _dayService.CreateDays(request.Param);
        return Unit.Value;
    }
}