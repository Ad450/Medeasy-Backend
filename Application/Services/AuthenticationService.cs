using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Dto;
using Application.Interfaces;
using Application.Utils;
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

    public async Task<object> Signin(AuthDto authDto)
    {
        var user = await _userManager.FindByEmailAsync(authDto.email)
            ?? throw new Exception("user not found");
        var isExactPassword = await _userManager.CheckPasswordAsync(user, authDto.password);
        if (!isExactPassword) throw new Exception("password does not match user");

        var roles = await _userManager.GetRolesAsync(user)
                ?? throw new Exception("no role found for user"); ;

        return await GetTokens(user, [.. roles]);
    }

    public Task Signout()
    {
        throw new Exception();
    }

    public async Task<object> RefreshToken(string expiredAccessToken, string oldRefreshToken)
    {
        var identity = await GetIdentityFromExpiredToken(expiredAccessToken);
        var userEmail = identity.Claims
            .Where(c => c.Type == ClaimTypes.Email)
            .Select(c => c.Value).ToList()
            .FirstOrDefault() ?? throw new Exception("could not find claim: Email in token");

        var user = await _userManager.FindByEmailAsync(userEmail)
                ?? throw new Exception("user not found");

        if (!IsRefreshTokenValid(user, oldRefreshToken)) throw new Exception("Token Invalid");

        var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

        return await GetTokens(user, roles);
    }

    private async Task<object> GetTokens(MedeasyUser user, List<string> roles)
    {
        var accessToken = GenerateJwtToken(user, roles);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(10);
        await _userManager.UpdateAsync(user);

        return new
        {
            AcessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    private bool IsRefreshTokenValid(MedeasyUser user, string refreshToken)
    {
        if (user.RefreshToken != refreshToken || DateTime.UtcNow > user.RefreshTokenExpiry)
            return false;
        return true;
    }

    private string? GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
    private string GenerateJwtToken(MedeasyUser user, IList<string> roles)
    {

        if (roles.IsNullOrEmpty()) throw new Exception("Add at least one role");
        IDictionary<string, object> claims = new Dictionary<string, object>
        {
            { ClaimTypes.Sid, user.Id},
            { ClaimTypes.Email, user.Email!},
        };
        foreach (var role in roles)
        {
            claims[ClaimTypes.Role] = role;
        }
        var securityTokenDescriptor = new SecurityTokenDescriptor()
        {
            Issuer = _configuration.GetSection("JWT:Issuer").Value,
            Expires = DateTime.UtcNow.AddSeconds(5),
            Audience = _configuration.GetSection("JWT:Audience").Value,
            Claims = claims,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:SigningCredentials").Value!)),
                SecurityAlgorithms.HmacSha256
            )

        };
        var token = new JsonWebTokenHandler().CreateToken(securityTokenDescriptor);
        return token;
    }

    private async Task<ClaimsIdentity> GetIdentityFromExpiredToken(string? token)
    {

        var validationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateLifetime = false,
            ValidateAudience = true,
            ValidIssuer = _configuration.GetSection("JWT:Issuer").Value,
            ValidAudience = _configuration.GetSection("JWT:Audience").Value,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey
                       (Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:SigningCredentials").Value!))
        };

        var tokenHandler = new JsonWebTokenHandler();
        var principal = await tokenHandler.ValidateTokenAsync(token, validationParameters);

        if (principal == null ||
            !principal.IsValid ||
            principal.SecurityToken.Issuer != _configuration.GetSection("JWT:Issuer").Value
        ) throw new Exception("Invalid token");

        return principal.ClaimsIdentity;

    }


}