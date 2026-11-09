using MediatR;
using MyWebApi.Domain.Entities;
using MyWebApi.Services.DTOs.Request;

namespace MyWebApi.Services.CQRS.Query.GetUsers;

public class GetUsersQuery : IRequest<ApiResponse<List<User>>>
{
    public int Page { get; set; }
    public int Size { get; set; }

    public GetUsersQuery(GetUsersQueryRequest query)
    {
        Page = query.Page;
        Size = query.Size;
    }
}