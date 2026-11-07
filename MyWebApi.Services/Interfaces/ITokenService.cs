using System.Security.Claims;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);

    ClaimsPrincipal VerifyToken(string token);
}