namespace ExaminationSystemDemo.Errors;

public static class ResultErrors
{
    public static Error StudentNotAllowedToGetResult = new("Sudent.StudentNotAllowedToGetResult", "you aren't allow to see result!");
    public static Error ResultNotFound = new("Result.ResultNotFound", "result not found");
}
