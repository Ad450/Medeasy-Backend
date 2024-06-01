using System.Security.Claims;
using System.Text;
using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enum;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;


namespace Application.Services;

public class AuthenticationService(
    UserManager<MedeasyUser> _userManager,
    IConfiguration _configuration,
    IBaseRepository<MedeasyUser> _userRepository,
    RoleManager<MedeasyRole> _roleManager
)
    : IAuthenticationService
{

    public async Task<string> InitializeRoles(IList<string> roles)
    {
        using var transaction = await _userRepository.GetContext().Database.BeginTransactionAsync();
        try
        {
            foreach (var role in roles)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    var result = await _roleManager.CreateAsync(new MedeasyRole { Name = role });
                    if (!result.Succeeded) throw new Exception("could not create roles");
                    await transaction.CommitAsync();
                }
            }
            return "Roles created";
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new Exception("Initializing roles", e);
        }

    }

    public async Task Register(AuthDto authDto)
    {
        using var transaction = await _userRepository.GetContext().Database.BeginTransactionAsync();
        try
        {
            var newUser = new MedeasyUser() { Email = authDto.email, UserName = authDto.email };
            var userExists = await _userManager.FindByEmailAsync(newUser.Email);
            if (userExists != null)
            {
                throw new Exception("user exists");
            }

            var result = await _userManager.CreateAsync(newUser, authDto.password);

            if (result.Succeeded)
            {
                foreach (UserRole role in authDto.Roles)
                {
                    var roleAdded = await _userManager.AddToRoleAsync(newUser, role.ToString());
                    if (!roleAdded.Succeeded)
                    {
                        await transaction.RollbackAsync();
                        throw new Exception("role was not added ");
                    }
                }
            }
            else
            {
                throw new Exception("new user creation faliled");
            }

            await transaction.CommitAsync();
            return;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new Exception("exception from registering user", e);
        }

    }

    public async Task<string> Signin(AuthDto authDto)
    {
        var user = await _userManager.FindByEmailAsync(authDto.email)
            ?? throw new Exception("user not found");
        var isExactPassword = await _userManager.CheckPasswordAsync(user, authDto.password);
        if (!isExactPassword) throw new Exception("password does not match user");

        var roles = await _userManager.GetRolesAsync(user)
                ?? throw new Exception("no role found for user"); ;

        var token = GenerateJWTToken(user, roles!);
        return token;
    }

    public async Task Signout()
    {
        throw new Exception();
    }

    private string GenerateJWTToken(MedeasyUser user, IList<string> roles)
    {
        IDictionary<string, object> claims = new Dictionary<string, object>
        {
            { ClaimTypes.Sid, user.Id},
        };
        foreach (var role in roles)
        {
            if (role != null)
            {
                claims[ClaimTypes.Role] = role;
            }
        }
        var securityTokenDescriptor = new SecurityTokenDescriptor()
        {
            Issuer = _configuration.GetSection("JWT:Issuer").Value,
            Expires = DateTime.UtcNow.AddDays(1),
            Audience = _configuration.GetSection("JWT:Audience").Value,
            Claims = claims,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:SigningCred").Value!)),
                SecurityAlgorithms.HmacSha256
            )

        };
        var token = new JsonWebTokenHandler().CreateToken(securityTokenDescriptor);
        return token;
    }
}