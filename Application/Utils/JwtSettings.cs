namespace Application.Utils;

public class JwtSettings
{
    public string SigningCredentials { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;

}
