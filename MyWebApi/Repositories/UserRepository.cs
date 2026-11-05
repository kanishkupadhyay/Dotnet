using MyWebApi.Repositories.Interfaces;

namespace MyWebApi.Repositories;

public class UserRepository : IUserRepository
{
    public Task<bool> IsEmailExists(string email)
    {
        // Implementation for checking if email exists
        return Task.FromResult(false);
    }

    public Task CreateUser(string firstName, string lastName, string email, string password)
    {
        // Implementation for creating user
        return Task.CompletedTask;
    }
}