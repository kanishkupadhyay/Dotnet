using MediatR;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Services.CQRS.Query.GetUsers;

public class GetUsersQuery : IRequest<ApiResponse<List<User>>>
{
}