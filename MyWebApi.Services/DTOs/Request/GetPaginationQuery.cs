namespace MyWebApi.Services.DTOs.Request;

public class GePaginationRequest
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
}