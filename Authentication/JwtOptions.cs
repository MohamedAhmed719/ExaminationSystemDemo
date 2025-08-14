namespace ExaminationSystemDemo.Authentication;
public class JwtOptions
{
    public static string SectionName = "Jwt";
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
    public int ExpiryMinuets { get; set; }
}

