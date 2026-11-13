using MediatR;
using MyWebApi.Services.DTOs.Request;
using MyWebApi.Services.DTOs.Response;

namespace MyWebApi.Services.CQRS.Query.GetUsers;

public class GetUsersQuery : IRequest<ApiResponse<List<UserResponse>>>
{
    public int Page { get; set; }
    public int Size { get; set; }

    public GetUsersQuery(GePaginationRequest query)
    {
        Page = query.Page;
        Size = query.Size;
    }
}