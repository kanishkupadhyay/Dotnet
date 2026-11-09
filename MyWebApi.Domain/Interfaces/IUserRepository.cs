using MyWebApi.Domain.Entities;

namespace MyWebApi.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    Task<List<User>> GetUsersAsync();

    Task AddAsync(User user);

    Task SaveChangesAsync();
}