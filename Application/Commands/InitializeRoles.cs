
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.Commands;

public class InitializeRolesCommand(InitializeRolesDto dto) : IRequest<string>
{
    public InitializeRolesDto Param { get; } = dto;
}


public class InitializeRolesHandler(IAuthenticationService _authenticationService) : IRequestHandler<InitializeRolesCommand, string>
{
    public async Task<string> Handle(InitializeRolesCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.InitializeRoles(request.Param.Roles);
    }
}