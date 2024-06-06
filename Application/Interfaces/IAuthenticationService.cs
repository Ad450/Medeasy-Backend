using Application.Dto;

namespace Application.Interfaces;

public interface IAuthenticationService
{
    public Task<string> InitializeRoles(IList<string> roles);
    public Task Register(AuthDto authDto);
    public Task<object> Signin(AuthDto authDto);
    public Task Signout();
    public Task<object> RefreshToken(string expiredAccessToken, string oldRefreshToken);
}