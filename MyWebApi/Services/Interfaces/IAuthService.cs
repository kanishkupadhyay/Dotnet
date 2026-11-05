using MyWebApi.DTOs.Request;
using MyWebApi.DTOs.Response;

namespace MyWebApi.Services.Interfaces;

public interface IAuthService
{
    Task Register(RegisterRequest request);
    Task<ApiResponse<object>> Login(LoginRequest request);
}