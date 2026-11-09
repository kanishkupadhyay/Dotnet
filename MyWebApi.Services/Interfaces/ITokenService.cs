using MyWebApi.Domain.Entities;

namespace MyWebApi.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}