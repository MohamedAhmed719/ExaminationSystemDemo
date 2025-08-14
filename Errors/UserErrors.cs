

namespace ExaminationSystemDemo.Errors;

public static class UserErrors
{
    public static Error EmailNotFound = new("User.EmailNotFound", "Email not found!");
    public static Error InvalidCredentials = new("User.InvalidCredentials", "Invalid username/password!");
    public static Error DuplicatedEmail = new("User.DuplicatedEmail", "Another user with the same email is already exists!");
    public static Error UserNotAllowed = new("User.UserNotAllowed", "you are not allow to update this exam");
}
