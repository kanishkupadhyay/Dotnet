using MediatR;
using MyWebApi.Services.DTOs.Request;
using MyWebApi.Services.DTOs.Response;

namespace MyWebApi.Services.CQRS.Command.UpdateBlogById;

public class UpdateBlogByIdCommand : IRequest<ApiResponse<BlogResponse>>
{
    public Guid Id {get; set;}
    public UpdateBlogRequest Command {get; set;}

    public UpdateBlogByIdCommand(Guid id, UpdateBlogRequest command)
    {
        Id = id;
        Command = command;
    }
}