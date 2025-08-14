namespace ExaminationSystemDemo.Authentication;

public interface IJwtProvider
{
    (string token, int ExpiresIn) GenerateJwtToken(ApplicationUser user,IEnumerable<string> roles);
}
