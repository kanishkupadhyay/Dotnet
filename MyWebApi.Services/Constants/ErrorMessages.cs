namespace MyWebApi.Services.Constants;

public static class ErrorMessages
{
    public const string EmailCannotBeNullOrEmpty = "Email cannot be null or empty";
    public const string InvalidEmail = "Invalid email address";
    public const string PasswordCannotBeNullOrEmpty = "Password cannot be null or empty";
    public const string ConfirmPasswordCannotBeNullOrEmpty = "Confirm password cannot be null or empty";
    public const string InvalidCredentials = "Invalid credentials";
    public const string InternalServerError = "An internal server error occurred";
    public const string FirstNameCannotBeNullOrEmpty = "First name cannot be null or empty";
    public const string PasswordsDoNotMatch = "Passwords do not match";
    public const string PasswordTooShort = "Password must be at least 8 characters long";
    public const string PasswordTooLong = "Password cannot exceed 100 characters";
    public const string UserAlreadyExists = "A user with this email already exists";

    // Blog related error messages
    public const string BlogNotFound = "Blog not found.";
    public const string TitleCannotBeNullOrEmpty = "Title cannot be null or empty.";
    public const string ContentCannotBeNullOrEmpty = "Content cannot be null or empty.";
    public const string UserIdNotAvailable = "User ID is not available.";
}
