namespace ExaminationSystemDemo.Contracts.Authentiaction;

public record AuthResponse(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string Token,
    int ExpiresIn
    );