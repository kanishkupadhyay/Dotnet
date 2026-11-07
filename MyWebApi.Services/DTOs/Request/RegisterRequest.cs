using System.ComponentModel.DataAnnotations;
using MyWebApi.Services.Constants;

namespace MyWebApi.DTOs.Request;

public class RegisterRequest
{
    [Required(ErrorMessage = ErrorMessages.FirstNameCannotBeNullOrEmpty)]
    [StringLength(50, MinimumLength = 2,
        ErrorMessage = "First name must be between 2 and 50 characters")]
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = ErrorMessages.EmailCannotBeNullOrEmpty)]
    [EmailAddress(ErrorMessage = ErrorMessages.InvalidEmail)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = ErrorMessages.PasswordCannotBeNullOrEmpty)]
    [MinLength(8, ErrorMessage = ErrorMessages.PasswordTooShort)]
    [MaxLength(100, ErrorMessage = ErrorMessages.PasswordTooLong)]
    public string Password { get; set; } = string.Empty;
    
    [Required(ErrorMessage = ErrorMessages.ConfirmPasswordCannotBeNullOrEmpty)]
    [Compare(nameof(Password),
       ErrorMessage = ErrorMessages.PasswordsDoNotMatch)]

    public string ConfirmPassword { get; set; } = string.Empty;
}