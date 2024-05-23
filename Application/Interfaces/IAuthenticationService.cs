using Application.Dto;

namespace Application.Interfaces;

public interface IAuthenticationService
{
    public Task Register(AuthDto authDto);
    public Task<string> Signin(AuthDto authDto);
    public Task Signout();
}