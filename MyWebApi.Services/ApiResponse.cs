using System.Text.Json.Serialization;

namespace MyWebApi.Services;

public class ApiResponse<T>
{
    public int Status { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }

    public static ApiResponse<T> BadRequest(string message)
    {
        return new ApiResponse<T>
        {
            Status = 400,
            Success = false,
            Message = message
        };
    }

    public static ApiResponse<T> NotFound(string message)
    {
        return new ApiResponse<T>
        {
            Status = 404,
            Success = false,
            Message = message
        };
    }

    public static ApiResponse<T> InternalServerError(string message)
    {
        return new ApiResponse<T>
        {
            Status = 500,
            Success = false,
            Message = message
        };
    }

    public static ApiResponse<T> OkResponse(string message, T? data = default)
    {
        return new ApiResponse<T>
        {
            Status = 200,
            Success = true,
            Message = message,
            Data = data
        };
    }

    public static ApiResponse<T> CreatedResponse(string message, T? data = default)
    {
        return new ApiResponse<T>
        {
            Status = 201,
            Success = true,
            Message = message,
            Data = data
        };
    }
}