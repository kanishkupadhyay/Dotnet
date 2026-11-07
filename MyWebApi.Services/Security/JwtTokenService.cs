using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyWebApi.Domain.Entities;
using MyWebApi.Services.Interfaces;

namespace MyWebApi.Services.Security;

public class JwtTokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");

        var secretKey = jwtSettings["Key"]
            ?? throw new InvalidOperationException(
                "JWT secret key is missing");

        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];

        if (string.IsNullOrWhiteSpace(issuer) ||
            string.IsNullOrWhiteSpace(audience))
        {
            throw new InvalidOperationException(
                "JWT issuer or audience is missing");
        }

        if (Encoding.UTF8.GetByteCount(secretKey) < 32)
        {
            throw new InvalidOperationException(
                "JWT secret key must be at least 32 bytes");
        }

        var expiresInMinutes =
            jwtSettings.GetValue<int>("ExpiresInMinutes");

        if (expiresInMinutes <= 0)
        {
            throw new InvalidOperationException(
                "JWT expiration must be greater than zero");
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        // User information
        var claims = new[]
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()),

            new Claim(
                ClaimTypes.Email,
                user.Email),

            new Claim(
                ClaimTypes.Name,
                user.FirstName)
        };

        // Create JWT
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                expiresInMinutes),
            signingCredentials: credentials
        );

        // Convert JWT object into string
        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }

    public ClaimsPrincipal VerifyToken(string token)
{
    var jwtSettings = _configuration.GetSection("Jwt");

    var secretKey = jwtSettings["Key"]
        ?? throw new InvalidOperationException(
            "JWT secret key is missing");

    var issuer = jwtSettings["Issuer"];
    var audience = jwtSettings["Audience"];

    var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(secretKey));

    var validationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = key,

        ClockSkew = TimeSpan.FromSeconds(30)
    };

    var handler = new JwtSecurityTokenHandler();

    var principal = handler.ValidateToken(
        token,
        validationParameters,
        out var validatedToken
    );

    // Ensure the token uses the expected signing algorithm
    if (validatedToken is not JwtSecurityToken jwtToken ||
        jwtToken.Header.Alg != SecurityAlgorithms.HmacSha256)
    {
        throw new SecurityTokenException(
            "Invalid token algorithm");
    }

    return principal;
}
}