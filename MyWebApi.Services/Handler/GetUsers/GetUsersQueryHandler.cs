using MediatR;
using MyWebApi.Domain.Entities;
using MyWebApi.Domain.Interfaces;
using MyWebApi.Services.Constants;

namespace MyWebApi.Services.CQRS.Query.GetUsers;

public class GetUsersQueryHandler: IRequestHandler<GetUsersQuery, ApiResponse<List<User>>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<List<User>>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetUsersAsync();
        return ApiResponse<List<User>>.OkResponse(SuccessMessages.UsersFetchedSuccessfully, users);
    }
}