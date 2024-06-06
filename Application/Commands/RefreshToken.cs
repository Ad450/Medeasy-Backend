
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.Commands;

public class RefreshTokenCommand(RefreshTokenDto dto) : IRequest<object>
{
    public RefreshTokenDto Param { get; } = dto;
}


public class RefreshTokenHandler(IAuthenticationService _authenticationService) : IRequestHandler<RefreshTokenCommand, object>
{
    public async Task<object> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.RefreshToken(request.Param.accessToken, request.Param.refreshToken);
    }
}