
using Application.Dto;
using MediatR;
using Application.Interfaces;

namespace Application.Commands;

public class SigninUserCommand(AuthDto authDto) : IRequest<object>
{
    public AuthDto Param { get; } = authDto;
}


public class SigninUserHandler(IAuthenticationService _authenticationService) : IRequestHandler<SigninUserCommand, object>
{
    public async Task<object> Handle(SigninUserCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.Signin(request.Param);
    }
}