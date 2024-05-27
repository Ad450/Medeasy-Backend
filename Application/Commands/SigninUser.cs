
using Application.Dto;
using MediatR;
using Application.Interfaces;

namespace Application.Commands;

public class SigninUserCommand(AuthDto authDto) : IRequest<string>
{
    public AuthDto Param = authDto;
}


public class SigninUserHandler(IAuthenticationService _authenticationService) : IRequestHandler<SigninUserCommand, string>
{
    public async Task<string> Handle(SigninUserCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.Signin(request.Param);
    }
}