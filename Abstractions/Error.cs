namespace ExaminationSystemDemo.Abstractions;

public record Error(string Code,string Description)
{
    public static Error none = new Error(string.Empty, string.Empty);
}
