using MediatR;
using MyWebApi.Domain.Interfaces;
using MyWebApi.Services.Constants;
using MyWebApi.Services.DTOs.Response;
using MyWebApi.Services.Mappers;

namespace MyWebApi.Services.CQRS.Query.GetUsers;

public class GetUsersQueryHandler: IRequestHandler<GetUsersQuery, ApiResponse<List<UserResponse>>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<List<UserResponse>>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetUsersAsync(query.Page, query.Size);
        var userResponses = users.Select(user => user.ToResponse()).ToList();

        return ApiResponse<List<UserResponse>>.OkResponse(SuccessMessages.UsersFetchedSuccessfully, userResponses);
    }
}
