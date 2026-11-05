namespace MyWebApi.DTOs.Request;

public class RegisterRequest
{
    public string firstName { get; set; } = string.Empty;
    public string lastName { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;
}