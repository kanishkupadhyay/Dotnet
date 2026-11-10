using MyWebApi.Domain.Entities;
using MyWebApi.Domain.Interfaces;
using MyWebApi.Services.CQRS.Query.GetUsers;
using MyWebApi.Services.DTOs.Request;
using MyWebApi.Services.DTOs.Response;

namespace MyWebApi.Tests;

public class GetUsersQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnUserResponseWithoutPasswordHash()
    {
        var users = new List<User>
        {
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane@example.com",
                PasswordHash = "super-secret-hash"
            }
        };

        var repository = new StubUserRepository(users);
        var handler = new GetUsersQueryHandler(repository);

        var result = await handler.Handle(new GetUsersQuery(new GetUsersQueryRequest()), CancellationToken.None);

        Assert.True(result.Success);
        Assert.NotNull(result.Data);

        var data = result.Data!;
        Assert.Single(data);
        Assert.Equal("jane@example.com", data[0].Email);
        Assert.DoesNotContain(typeof(UserResponse).GetProperties(), property => property.Name == "PasswordHash");
        Assert.Equal("Jane", data[0].FirstName);
    }

    private sealed class StubUserRepository(List<User> users) : IUserRepository
    {
        public Task<User?> GetByEmailAsync(string email) => Task.FromResult(users.FirstOrDefault(u => u.Email == email));

        public Task<List<User>> GetUsersAsync(int page, int size) => Task.FromResult(users);

        public Task AddAsync(User user) => Task.CompletedTask;

        public Task SaveChangesAsync() => Task.CompletedTask;
    }
}
