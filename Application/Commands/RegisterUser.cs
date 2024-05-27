
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.Commands;

public class RegisterUserCommand(AuthDto authDto) : IRequest<Unit>
{
    public AuthDto Param { get; } = authDto;
}


public class RegisterUserHandler(IAuthenticationService _authenticationService) : IRequestHandler<RegisterUserCommand, Unit>
{
    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        await _authenticationService.Register(request.Param);
        return Unit.Value;
    }
}