namespace MyWebApi.Repositories.Interfaces;

public interface IUserRepository
{
    Task<bool> IsEmailExists(string email);
    Task CreateUser(string firstName, string lastName, string email, string password);
}