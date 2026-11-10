namespace MyWebApi.Services.Interfaces;

public interface ICurrentUserService
{
    Guid? UserId { get; }
}